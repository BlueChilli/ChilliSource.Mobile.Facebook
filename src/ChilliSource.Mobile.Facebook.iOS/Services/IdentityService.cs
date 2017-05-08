#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Facebook.CoreKit;
using Facebook.LoginKit;
using Foundation;
using Newtonsoft.Json.Linq;
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.Facebook;

[assembly: Xamarin.Forms.Dependency(typeof(IdentityService))]
namespace ChilliSource.Mobile.Facebook
{
	public class IdentityService : IIdentityService
	{
		private readonly Lazy<LoginManager> _manager;
		readonly List<string> _readPermissions = new List<string> { "public_profile", "email" };

		public IdentityService()
		{
			_manager = new Lazy<LoginManager>(() =>
			{
				var manager = new LoginManager();
				manager.Init();
				return manager;
			});
		}

		public async Task<OperationResult<FacebookSession>> Login(List<string> fields, List<string> permissions = null)
		{
			var taskCompletionSource = new TaskCompletionSource<OperationResult<FacebookSession>>();
			try
			{
				var session = this.GetSession();

				if (session == null)
				{
					if (permissions == null)
					{
						permissions = _readPermissions;
					}

					try
					{
						var result = await _manager.Value.LogInWithReadPermissionsAsync(permissions.ToArray(), null);

						if (result.IsCancelled)
						{
							taskCompletionSource.SetResult(OperationResult<FacebookSession>.AsCancel());
						}
						else if (result.Token != null)
						{
							session = GetSessionToken(result.Token);
						}
						else
						{
							taskCompletionSource.SetResult(OperationResult<FacebookSession>.AsFailure("Token is not available"));
						}
					}
					catch (Exception ex)
					{
						taskCompletionSource.SetResult(OperationResult<FacebookSession>.AsFailure(ex));
					}
				}

				if (session != null)
				{
					GetUserInfo(fields, session, taskCompletionSource);
				}

			}
			catch (AggregateException ex)
			{
				taskCompletionSource.SetResult(OperationResult<FacebookSession>.AsFailure(ex));
			}

			return await taskCompletionSource.Task;
		}

		public async Task<OperationResult<FacebookSession>> GetUserInfo(string token, List<string> fields)
		{
			var taskCompletionSource = new TaskCompletionSource<OperationResult<FacebookSession>>();

			GetUserInfo(fields, new FacebookSession()
			{
				Token = token
			}, taskCompletionSource);

			return await taskCompletionSource.Task;
		}

		public FacebookSession GetSession()
		{
			return AccessToken.CurrentAccessToken != null ? GetSessionToken(AccessToken.CurrentAccessToken) : null;
		}

		public void Logout()
		{
			_manager.Value.LogOut();
		}

		#region Private Methods

		private void GetUserInfo(List<string> fields, FacebookSession facebookSession, TaskCompletionSource<OperationResult<FacebookSession>> taskCompletionSource)
		{
			var builder = new StringBuilder();

			fields.ForEach(m =>
			{
				builder.Append($"{m},");
			});


			var s = builder.ToString();

			if (!String.IsNullOrWhiteSpace(s))
			{
				s = s.Substring(0, s.Length - 1);
			}


			var graphRequest = new GraphRequest($"/me?fields={s}", null, facebookSession.Token, null, "GET");
			var requestConnection = new GraphRequestConnection();
			requestConnection.AddRequest(graphRequest, (connection, result, error) =>
			{
				if (error != null)
				{
					taskCompletionSource.SetResult(
						OperationResult<FacebookSession>.AsFailure(new ApplicationException(error.LocalizedDescription)));
					return;
				}

				var r = result as NSDictionary;
				var jsonData = r.ToDictionary();
				facebookSession.Data = JObject.FromObject(jsonData);

				taskCompletionSource.SetResult(OperationResult<FacebookSession>.AsSuccess(facebookSession));
			});

			requestConnection.Start();
		}


		private FacebookSession GetSessionToken(AccessToken accessToken)
		{
			var session = new FacebookSession()
			{
				Token = accessToken.TokenString,
				UserId = accessToken.UserID,
			};

			var permissions = accessToken.Permissions.ToArray<NSString>();

			foreach (var m in permissions)
			{
				session.Permissions.Add(m.ToString());
			}

			return session;
		}

		#endregion
	}
}

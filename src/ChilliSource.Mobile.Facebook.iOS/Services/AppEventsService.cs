#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.Facebook;
using Facebook.CoreKit;

[assembly: Xamarin.Forms.Dependency(typeof(AppEventsService))]
namespace ChilliSource.Mobile.Facebook
{
	public class AppEventsService : IAppEventsService
	{
		public AppEventsService()
		{
		}

		public void ActivateApp()
		{
			AppEvents.ActivateApp();
		}

		public Task<OperationResult<string>> FetchDeferredAppLink()
		{
			var tcs = new TaskCompletionSource<OperationResult<string>>();

			AppLinkUtility.FetchDeferredAppLink((url, error) =>
			{
				if (error != null)
				{
					tcs.SetResult(OperationResult<string>.AsFailure(error.LocalizedDescription));
				}
				else
				{
					if (url == null)
					{
						tcs.SetResult(OperationResult<string>.AsFailure("Returned Url is null"));
					}
					else
					{
						tcs.SetResult(OperationResult<string>.AsSuccess(url.ToString()));
					}

				}
			});

			return tcs.Task;
		}


		public void LogEvent(string eventName)
		{
			AppEvents.LogEvent(eventName);
		}
	}
}

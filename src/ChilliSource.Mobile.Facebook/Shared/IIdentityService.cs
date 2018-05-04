#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChilliSource.Mobile.Core;

namespace ChilliSource.Mobile.Facebook
{
	/// <summary>
	/// Provides access to the Facebook user management functionality
	/// </summary>
	public interface IIdentityService
	{
		/// <summary>
		/// Prompt the user to log in to Facebook and request access to the information specified by <paramref name="permissions"/>
		/// </summary>
		/// <returns><see cref="T:ChilliSource.Mobile.Core.OperationResult"/> instance indicating the outcome
		/// of the operation and holding a <see cref="T:ChilliSource.Mobile.Facebook.FacebookSession"/> instance
		/// with the login response data</returns>
		/// <param name="fields">User fields that should be retrieved</param>
		/// <param name="permissions">Facebook profile permissions to be requested to the user.
		/// Default is public profile and email</param>
		Task<OperationResult<FacebookSession>> Login(List<string> fields, List<string> permissions = null);


		/// <summary>
		/// Invokes the Facebook Graph API to retrieve the user <paramref name="fields"/> for the active user session
		/// </summary>
		/// <returns><see cref="T:ChilliSource.Mobile.Core.OperationResult"/> instance indicating the outcome
		/// of the operation and holding a <see cref="T:ChilliSource.Mobile.Facebook.FacebookSession"/> instance
		/// with the requested user fields</returns>
		/// <param name="token">Token representing the active user session</param>
		/// <param name="fields">User fields that should be retrieved.</param>
		Task<OperationResult<FacebookSession>> GetUserInfo(string token, List<string> fields);

		/// <summary>
		/// Logs out the Facebook user and ends the session
		/// </summary>
		void Logout();

		/// <summary>
		/// Retrieves the active user session
		/// </summary>
		/// <returns><see cref="T:ChilliSource.Mobile.Facebook.FacebookSession"/> instance holding the session info</returns>
		FacebookSession GetSession();
	}
}

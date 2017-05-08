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

namespace ChilliSource.Mobile.Facebook
{
	/// <summary>
	/// Provides methods to the Facebook analytics features
	/// </summary>
	public interface IAppEventsService
	{
		/// <summary>
		/// Needs to be called by the app at startup to initialize 
		/// the Facebook analytics functionality
		/// </summary>
		void ActivateApp();

		/// <summary>
		/// Fetchs the Facebook deferred deep link.
		/// </summary>
		/// <returns><see cref="T:ChilliSource.Mobile.Core.OperationResult"/> instance indicating the outcome
		///  of the operation and holding the link</returns>
		Task<OperationResult<string>> FetchDeferredAppLink();

		/// <summary>
		/// Logs a custom event to Facebook analytics
		/// </summary>
		/// <param name="eventName">Event name.</param>
		void LogEvent(string eventName);
	}
}

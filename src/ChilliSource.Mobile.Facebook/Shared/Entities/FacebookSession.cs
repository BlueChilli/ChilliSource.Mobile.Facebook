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
using Newtonsoft.Json.Linq;

namespace ChilliSource.Mobile.Facebook
{
	/// <summary>
	/// Stores Facebook user login response data
	/// </summary>
	public class FacebookSession
	{
		public FacebookSession()
		{
			Permissions = new List<string>();
		}

		/// <summary>
		/// Unique login session identifier
		/// </summary>
		/// <value>The token.</value>
		public string Token { get; set; }

		/// <summary>
		/// The user's Facebook id
		/// </summary>
		/// <value>The user identifier.</value>
		public string UserId { get; set; }

		/// <summary>
		/// The permissions granted by the user
		/// </summary>
		/// <value>The permissions.</value>
		public List<string> Permissions { get; set; }

		/// <summary>
		/// Json data holding the user fields requested on login
		/// </summary>
		public JObject Data { get; set; }
	}
}

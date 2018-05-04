#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using ChilliSource.Mobile.Core;

namespace ChilliSource.Mobile.Facebook
{
	/// <summary>
	/// Provides methods to share media items to Facebook
	/// </summary>
	public interface ISharingService
	{
		/// <summary>
		/// Shows a Facebook popup prompting the user to share the specified <paramref name="link"/>
		/// </summary>
		/// <returns><see cref="T:ChilliSource.Mobile.Core.OperationResult"/> instance indicating the outcome
		///  of the operation</returns>
		/// <param name="link">The url to be shared</param>
		/// <param name="hashtags">Optional hashtags</param>
		OperationResult ShareLink(string link, List<string> hashtags = null);

		/// <summary>
		/// Shows a Facebook popup prompting the user to share the specified <paramref name="videoPath"/>.
		/// Note: the path has to point to an asset in the media library. Use the <see cref="T:ChilliSource.Mobile.Media.IMediaService"/>
		/// to retrieve the media library url for an asset.
		/// </summary>
		/// <returns><see cref="T:ChilliSource.Mobile.Core.OperationResult"/> instance indicating the outcome
		///  of the operation</returns>
		/// <param name="videoPath">Media library video path</param>
		/// <param name="hashtags">Optional hashtags</param>
		OperationResult ShareVideoFile(string videoPath, List<string> hashtags = null);

		/// <summary>
		/// Shows a Facebook popup prompting the user to share the specified <paramref name="imagePaths"/>.
		/// Note: the paths have to point to assets in the media library. Use the <see cref="T:ChilliSource.Mobile.Media.IMediaService"/>
		/// to retrieve the media library urls for the assets.
		/// </summary>
		/// <returns><see cref="T:ChilliSource.Mobile.Core.OperationResult"/> instance indicating the outcome
		///  of the operation</returns>
		/// <param name="imagePaths">List of media library image paths</param>
		/// <param name="hashtags">Optional hashtags</param>
		OperationResult ShareImageFiles(List<string> imagePaths, List<string> hashtags = null);
	}
}

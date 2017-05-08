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
using ChilliSource.Mobile.Facebook;
using ChilliSource.Mobile.UI.Core;
using Facebook.ShareKit;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(SharingService))]

namespace ChilliSource.Mobile.Facebook
{
	public class SharingService : ISharingService
	{
		public OperationResult ShareLink(string link, List<string> hashtags = null)
		{
			var content = new ShareLinkContent();
			content.SetContentUrl(NSUrl.FromString(link));

			AddTagsToContent(content, hashtags);

			return ShowSharingSheet(content);
		}

		public OperationResult ShareVideoFile(string assetPath, List<string> hashtags = null)
		{
			var url = new NSUrl(assetPath);
			var video = ShareVideo.From(url);
			var content = new ShareVideoContent();
			content.Video = video;

			AddTagsToContent(content, hashtags);

			return ShowSharingSheet(content);
		}

		public OperationResult ShareImageFiles(List<string> assetPaths, List<string> hashtags = null)
		{
			List<SharePhoto> sharePhotos = new List<SharePhoto>();
			foreach (var path in assetPaths)
			{
				var url = new NSUrl(path);
				sharePhotos.Add(SharePhoto.From(url, true));
			}

			var content = new SharePhotoContent();
			content.Photos = sharePhotos.ToArray();

			AddTagsToContent(content, hashtags);

			return ShowSharingSheet(content);
		}

		#region Helper Methods

		private void AddTagsToContent(ISharingContent content, List<string> hashtags)
		{
			if (hashtags != null)
			{
				foreach (var hashtag in hashtags)
				{
					content.SetHashtag(new Hashtag() { StringRepresentation = hashtag });
				}
			}
		}

		private OperationResult ShowSharingSheet(ISharingContent content)
		{
			var dialog = new ShareDialog();
			dialog.FromViewController = NavigationService.GetActiveViewController();
			dialog.SetShareContent(content);
			dialog.Mode = ShareDialogMode.ShareSheet;
			bool success = dialog.Show();

			if (success)
			{
				return OperationResult.AsSuccess();
			}
			else
			{
				return OperationResult.AsFailure("Could not display Facebook sharing sheet");
			}
		}

		#endregion

	}
}

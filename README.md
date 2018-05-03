[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT) ![Built With C#](https://img.shields.io/badge/Built_with-C%23-green.svg)

# ChilliSource.Mobile.Facebook #

This project is part of the ChilliSource framework developed by [BlueChilli](https://github.com/BlueChilli).

## Summary ##

```ChilliSource.Mobile.Facebook``` provides simplified access to Facebook's identity and media sharing features. 

## Usage ##

### Identity ###

To use Facebook's login and identity functionality, first initialize the ```ChilliSource.Mobile.Facebook.IIdentityService``` dependency service:
```csharp
var identityService = DependencyService.Get<IIdentityService>();
```

You must also add the following code to your ```AppDelegate.cs``` for iOS:
```csharp
public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
{
    // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication work.
    return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
}
```

**Logging In**

```csharp
var permissions = { "public_profile", "email" };
var fields = { "id", "name", "email", "gender", "age_range", "first_name", "last_name", 
    "is_verified", "verified", "picture" };

var result = await identityService.Login(fields, permissions);
if (result.IsSuccessful)
{
    var token = result.Result.Token;
    Console.WriteLine("Facebook token:  " + token);
}
```

This will present the standard Facebook login page and return back to the application once the user has logged in.

**Logging Out**
```csharp
identityService.Logout();
```

**Retrieving User Info**

```csharp
var userInfoResult = await Global.Instance.FacebookIdentityProvider.GetUserInfo(token, fields);
if (userInfoResult.IsSuccessful)
{
    JObject data = userInfoResult.Result.Data;
    var name = data["name"].ToString();
    var facebookId = data["id"].ToString();
}
```

### Sharing ###

To use Facebook's media sharing functionality, first initialize the ```ChilliSource.Mobile.Facebook.ISharingService``` dependency service:
```csharp
var sharingService = DependencyService.Get<ISharingService>();
```

Invoking any of the sharing methods below will present a Facebook popup prompting the user to share the specified link/video/images.

**Sharing a Link**

```csharp
var result = sharingService.ShareLink(linkUrl, hashtags);
```

**Sharing Images**

You can share multiple images at the same time by passing in a List of string paths.

```csharp
var result = sharingService.ShareImageFiles(imagePaths, hashtags);
```

Note that the image paths have to point to assets in the media library. Use the ```ChilliSource.Mobile.Media.IMediaService``` dependency service to retrieve the media library url for an asset.

**Sharing a Video**

```csharp
var result = sharingService.ShareVideoFile(videoPath, hashtags);
```

Note that the video path has to point to an asset in the media library. Use the ```ChilliSource.Mobile.Media.IMediaService``` dependency service to retrieve the media library url for an asset.

### Analytics ###

To use Facebook's ```AppEvents``` to log analytics data, first initialize the ```ChilliSource.Mobile.Facebook.IAppEventsService``` dependency service:
```csharp
var appEventsService = DependencyService.Get<IAppEventsService>();
```

Then add the following code in your ```AppDelegate.cs``` for iOS:
```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    ...

    if (options == null || options[UIApplication.LaunchOptionsUrlKey] == null)
    {
        var result = await appEventsService.FetchDeferredAppLink();
        if (result.IsSuccessful)
        {
            UIApplication.SharedApplication.OpenUrl(NSUrl.FromString(result.Result));
        }
    }
    ...
}
```

```csharp
public override void OnActivated(UIApplication uiApplication)
{
    appEventsService?.ActivateApp();
}
```

To log custom events simply call:
```csharp
appEventsService.LogEvent(eventTitle);
```

## Installation ##

The library is available via NuGet [here](https://www.nuget.org/packages/ChilliSource.Mobile.Api).

## Releases ##

See the [releases](https://github.com/BlueChilli/ChilliSource.Mobile.Facebook/releases).

## Contribution ##

Please see the [Contribution Guide](.github/CONTRIBUTING.md).

## License ##

ChilliSource.Mobile is licensed under the [MIT license](LICENSE).

## Feedback and Contact ##

For questions or feedback, please contact [chillisource@bluechilli.com](mailto:chillisource@bluechilli.com).



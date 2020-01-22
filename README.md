## Plugin.DBChooser

Plugin.DBChooser is a Xamarin plugin that enables you to use the Dropbox Chooser Api
on Android and iOs
https://www.dropbox.com/developers/chooser

To intialize the Api use the following code

### Android

Add to your on Create Method the following line, where xxxxxxxxxxxxxxx is your Dropbox APP_KEY

	using Plugin.DBChooser;

        protected override void OnCreate(Bundle bundle)
        {
            ...
            CrossDBChooser.Current.initWithAppKey(this, "xxxxxxxxxxxxxxx");
        }

Add to your onActivityResult the following line

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            .......
            CrossDBChooser.Current.parseDropboxChooserResult(requestCode, resultCode, data);
        }

### iOs

Add to your info.plist the following lines

	<key>LSApplicationQueriesSchemes</key>
	<array>
		<string>dbapi-1</string>
		<string>dbapi-3</string>
	</array>

And a custom URL Scheme graphically in the URL Schemes section, or modifiying your info.plist where xxxxxxxxxxxxxxx is your Dropbox APP_KEY

	<key>CFBundleURLTypes</key>
	<array>
		<dict>
			<key>CFBundleTypeRole</key>
			<string>Editor</string>
			<key>CFBundleURLSchemes</key>
			<array>
				<string>db-xxxxxxxxxxxxxxx</string>
			</array>
			<key>CFBundleURLName</key>
			<string>com.LugertVerlag.ForteReader</string>
		</dict>
	</array>

For more information check the dropbox chooser api iOs documentation
https://www.dropbox.com/developers/chooser#ios

Add the following line to your AppDelegate.cs overriding the OpenUrl method

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return CrossDBChooser.Current.handleUrl(url);
        }

### USE

To Launch the Dropbox Chooser Api and download a file just use the following code in your
Xamarin.Forms part:

### Example

        private void openDropbox()
        {
            CrossDBChooser.Current.openDropboxChooser(DropboxChooserLinkType.Direct, onDropboxResult);
        }

        private async void onDropboxResult(DropboxChooserResult chooserResult) 
        {
            string URL = "https://dl.dropboxusercontent.com/" + chooserResult.Link;
            HttpClient hTTPClient = new HttpClient();
            var response = await hTTPClient.GetAsync(new Uri(URL));
            if (response.IsSuccessStatusCode)
            {
                byte[] content = await response.Content.ReadAsByteArrayAsync();
                //Do Something with the content
            }
        }



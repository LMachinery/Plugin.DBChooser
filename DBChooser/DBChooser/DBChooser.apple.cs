using Dropins.Chooser.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Plugin.DBChooser
{
    /// <summary>
    /// Interface for DBChooser
    /// </summary>
    public class DBChooserImplementation : IDBChooser
    {
        private onDBChooserResult chooserResult;

        public void initWithAppKey(object activity, string AppKey)
        {
            
        }

        public void openDropboxChooser(DropboxChooserLinkType linkType, onDBChooserResult chooserResult)
        {
            this.chooserResult = chooserResult;
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }
            DBChooserLinkType linkTypeIOs = DBChooserLinkType.Direct;
            if (linkType == DropboxChooserLinkType.Preview)
            {
                linkTypeIOs = DBChooserLinkType.Preview;
            }
            Dropins.Chooser.iOS.DBChooser.DefaultChooser.OpenChooser(linkTypeIOs, vc, new DBChooserCompletionHandler(
            (DBChooserResult[] results) =>
            {
                System.Diagnostics.Debug.WriteLine("Results . . . . . ");
                if (results != null)
                {
                    if (results.Length > 0)
                    {
                        DBChooserResult result0 = results[0];
                        Dictionary<string, Uri> thumbnails = new Dictionary<string, Uri>();
                        if (result0.Thumbnails != null)
                        {
                            foreach (NSObject curKey in result0.Thumbnails.Keys)
                            {
                                NSString strKey = (NSString)curKey;
                                thumbnails.Add((string)strKey, new Uri(((NSUrl)result0.Thumbnails[strKey]).Path));
                            }
                        }
                        string link = null;
                        if (result0.Link != null)
                        {
                            link = result0.Link.Path;
                        }
                        string icon = null;
                        if (result0.IconUrl != null)
                        {
                            icon = result0.IconUrl.Path;
                        }
                        DropboxChooserResult droboxChooserResult = new DropboxChooserResult(link, result0.Name, icon, result0.Size, thumbnails);
                        this.chooserResult(droboxChooserResult);
                    }
                }
            }
            ));
        }

        public void parseDropboxChooserResult(int requestCode, object resultCode, object data) 
        {
            
        }

        public bool handleUrl(object url)
        {
            return Dropins.Chooser.iOS.DBChooser.DefaultChooser.HandleOpenUrl((NSUrl)url);
        }
    }
}
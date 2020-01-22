using Android.App;
using Android.Content;
using Com.Dropbox.Chooser.Android;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DBChooser
{
    /// <summary>
    /// Interface for DBChooser
    /// </summary>
    public class DBChooserImplementation : IDBChooser
    {
        private string AppKey = "";
        private Activity activity;
        private int DBX_CHOOSER_REQUEST = 1020;
        private onDBChooserResult chooserResult;

        public void initWithAppKey(object activity, string AppKey)
        {
            this.AppKey = AppKey;
            this.activity = (Activity)activity;
        }

        public void openDropboxChooser(DropboxChooserLinkType linkType, onDBChooserResult chooserResult)
        {
            this.chooserResult = chooserResult;
            DbxChooser.ResultType resultType = DbxChooser.ResultType.DirectLink;
            if (linkType == DropboxChooserLinkType.Preview)
            {
                resultType = DbxChooser.ResultType.PreviewLink;
            }
            DbxChooser mChooser = new DbxChooser(AppKey);
            mChooser.ForResultType(resultType)
                    .Launch(activity, DBX_CHOOSER_REQUEST);
        }

        public void parseDropboxChooserResult(int requestCode, object resultCode, object data)
        {
            Result resultCode0 = (Result)resultCode;
            Intent intentObj = (Intent)data;
            if (requestCode == DBX_CHOOSER_REQUEST)
            {
                if (resultCode0 == Result.Ok)
                {
                    DbxChooser.Result result = new DbxChooser.Result(intentObj);
                    Dictionary<string, Uri> thumbnails = new Dictionary<string, Uri>();
                    if (result.Thumbnails != null)
                    {
                        foreach (string curKey in result.Thumbnails.Keys)
                        {
                            thumbnails.Add(curKey, new Uri(result.Thumbnails[curKey].Path));
                        }
                    }
                    string link = null;
                    if (result.Link != null) 
                    {
                        link = result.Link.Path;
                    }
                    string icon = null;
                    if (result.Icon != null) 
                    {
                        icon = result.Icon.Path;
                    }
                    DropboxChooserResult droboxChooserResult = new DropboxChooserResult(link, result.Name, icon, result.Size, thumbnails);
                    this.chooserResult(droboxChooserResult);
                }
            }
        }

        public bool handleUrl(object url)
        {
            return false;
        }
    }
}

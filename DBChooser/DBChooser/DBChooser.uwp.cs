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
        public bool handleUrl(object url)
        {
            throw new NotImplementedException();
        }

        public void initWithAppKey(object activity, string AppKey)
        {
            throw new NotImplementedException();
        }

        public void parseDropboxChooserResult(int requestCode, object resultCode, object data)
        {
            throw new NotImplementedException();
        }

        public void openDropboxChooser(DropboxChooserLinkType linkType, onDBChooserResult chooserResult)
        {
            throw new NotImplementedException();
        }
    }
}

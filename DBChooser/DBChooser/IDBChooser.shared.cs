using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DBChooser
{
    public interface IDBChooser
    {
        public void initWithAppKey(object activity, string AppKey);
        void openDropboxChooser(DropboxChooserLinkType linkType, onDBChooserResult chooserResult);
        public void parseDropboxChooserResult(int requestCode, object resultCode, object data);
        public bool handleUrl(object url);
    }

    public enum DropboxChooserLinkType
    {
        Preview,
        Direct
    }

    public delegate void onDBChooserResult(DropboxChooserResult droboxChooserResult);

    public class DropboxChooserResult
    {
        private string link;
        private string name;
        private string icon;
        private long size;
        private Dictionary<String, Uri> thumbnails;

        public DropboxChooserResult(string link, string name, string icon, long size, Dictionary<string, Uri> thumbnails)
        {
            this.link = link;
            this.name = name;
            this.icon = icon;
            this.size = size;
            this.thumbnails = thumbnails;
        }

        public string Link { get => link; }
        public string Name { get => name; }
        public string Icon { get => icon; }
        public long Size { get => size; }
        public Dictionary<string, Uri> Thumbnails { get => thumbnails; }
    }
}

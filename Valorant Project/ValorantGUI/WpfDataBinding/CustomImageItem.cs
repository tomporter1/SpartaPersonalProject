using System;
using System.Windows.Media.Imaging;

namespace ValorantGUI
{
    public class CustomImageItem
    {
        private object _obj;
        private BitmapImage _image;

        public CustomImageItem(object obj, string imagePath)
        {
            _obj = obj;
            _image = imagePath != null && imagePath != "" ? new BitmapImage(new Uri(imagePath, UriKind.Relative)) : null;
        }

        public string Text { get => _obj.ToString(); }
        public BitmapImage Image { get => _image; }
        public object Obj { get => _obj; }
    }
}
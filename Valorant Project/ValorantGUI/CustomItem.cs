using System;
using System.Windows.Media.Imaging;

namespace ValorantGUI
{
    public class CustomItem
    {
        private object _obj;
        private BitmapImage _image;

        public CustomItem(object obj, string imagePath)
        {
            _obj = obj;

            BitmapImage src = new BitmapImage();
            //src.BeginInit();
            _image = imagePath != null && imagePath != "" ? new BitmapImage(new Uri(imagePath, UriKind.Relative)) : null;
            // src.EndInit();
            //_image = src;
        }

        public string Text { get => _obj.ToString(); }
        public BitmapImage Image { get => _image; }
        public object Obj { get => _obj; }
    }
}
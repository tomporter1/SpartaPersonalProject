using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ValorantGUI
{
    public class CustomBackgroundItem
    {
        private object _obj;
        private SolidColorBrush _bgColour;
        private BitmapImage _rankImage;
        private BitmapImage _adjustmentImage;
        private const int _imageSize = 25;

        public CustomBackgroundItem(object obj, Color colour, string rankImagePath, string rankAdjustmentImagePath)
        {
            _obj = obj;
            _bgColour = new SolidColorBrush(colour);
            _rankImage = rankImagePath != null && rankImagePath != "" ? new BitmapImage(new Uri(rankImagePath, UriKind.Relative)) : null;
            _adjustmentImage = rankAdjustmentImagePath != null && rankAdjustmentImagePath != "" ? new BitmapImage(new Uri(rankAdjustmentImagePath, UriKind.Relative)) : null;
        }

        public object Obj { get => _obj; }
        public string Text { get => _obj.ToString(); }
        public SolidColorBrush Background { get => _bgColour; }
        public BitmapImage RankImage { get => _rankImage; }
        public BitmapImage AdjustmentImage { get => _adjustmentImage; }
        public int RankImageSize { get => _rankImage == null ? 0 : _imageSize; }
        public int AdjustmentImageSize { get => _adjustmentImage == null ? 0 : _imageSize; }
    }
}
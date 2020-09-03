using System.Windows.Media;

namespace ValorantGUI
{
    public class CustomBackgroundItem
    {
        private object _obj;
        private SolidColorBrush _bgColour;

        public CustomBackgroundItem(object obj, Color colour)
        {
            _obj = obj;
            _bgColour = new SolidColorBrush(colour);
        }

        public object Obj { get => _obj; }
        public string Text { get => _obj.ToString(); }
        public SolidColorBrush Background { get => _bgColour; }
    }
}
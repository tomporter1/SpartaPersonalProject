using System.Windows;
using System.Windows.Input;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindow
    {
        private MainPage _mainPage;
        private Cursor _previousCursor;

        public MainWindow()
        {
            InitializeComponent();
            SetHomePage();
        }

        public void SetHomePage()
        {
            if (_mainPage == null)
                _mainPage = new MainPage(this);

            Content = _mainPage;
        }

        public void WaitMouse()
        {
            _previousCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
        }

        public void EndWaitMouse()
        {
            Mouse.OverrideCursor = _previousCursor;
        }

        public void SetContent(object newPage) => Content = newPage;
    }
}
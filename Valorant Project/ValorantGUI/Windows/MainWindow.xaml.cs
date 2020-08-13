using System.Windows;
using System.Windows.Input;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainPage _mainPage;
        private Cursor _previousCursor;

        public MainWindow()
        {
            InitializeComponent();
            SetHomePage();
        }

        internal void SetHomePage()
        {
            if (_mainPage == null)
                _mainPage = new MainPage(this);

            Content = _mainPage;
        }

        internal void WaitMouse()
        {
            _previousCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
        }

        internal void EndWaitMouse()
        {
            Mouse.OverrideCursor = _previousCursor;
        }
    }
}
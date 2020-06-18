using System.Windows;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainPage _mainPage;

        public MainWindow()
        {
            InitializeComponent();
            SetHomePage();
        }

        internal void SetHomePage()
        {
            if (_mainPage == null)
            {
                _mainPage = new MainPage(this);
                Content = _mainPage;
            }
            else
            {
                Content = _mainPage;
            }
        }
    }
}

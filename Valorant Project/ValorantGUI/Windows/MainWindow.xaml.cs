using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        MainWindow _mainWindow;

        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void AgentsButton_Click(object sender, RoutedEventArgs e)
        {
            AgentsPage agentsPage = new AgentsPage(_mainWindow, this);
            _mainWindow.Content = agentsPage;
        }

        private void MapsButton_Click(object sender, RoutedEventArgs e)
        {
            MapsPage mapsPage = new MapsPage(_mainWindow, this);
            _mainWindow.Content = mapsPage;
        }
    }
}

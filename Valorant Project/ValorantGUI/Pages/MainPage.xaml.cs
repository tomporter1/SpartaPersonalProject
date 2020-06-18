using System.Windows;
using System.Windows.Controls;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        MainWindow _window;

        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _window = mainWindow;
        }

        private void AgentsButton_Click(object sender, RoutedEventArgs e)
        {
            AgentsPage agentsPage = new AgentsPage(_window);
            _window.Content = agentsPage;
        }

        private void MapsButton_Click(object sender, RoutedEventArgs e)
        {
            MapsPage mapsPage = new MapsPage(_window);
            _window.Content = mapsPage;
        }

        private void ClassesButton_Click(object sender, RoutedEventArgs e)
        {
            AgentClassesPage classesPage = new AgentClassesPage(_window);
            _window.Content = classesPage;
        }
        
        private void GameLogButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogPage gamessPage = new GameLogPage(_window);
            _window.Content = gamessPage;
        }
    }
}

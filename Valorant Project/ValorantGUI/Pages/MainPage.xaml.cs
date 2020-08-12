using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private MainWindow _window;

        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _window = mainWindow;
        }

        private void AgentsButton_Click(object sender, RoutedEventArgs e)
        {
            _window.WaitMouse();

            AgentsPage agentsPage = new AgentsPage(_window);
            _window.Content = agentsPage;

            _window.EndWaitMouse();
        }       

        private void MapsButton_Click(object sender, RoutedEventArgs e)
        {
            _window.WaitMouse();
       
            MapsPage mapsPage = new MapsPage(_window);
            _window.Content = mapsPage;

            _window.EndWaitMouse();
        }

        private void ClassesButton_Click(object sender, RoutedEventArgs e)
        {
            _window.WaitMouse();
     
            AgentClassesPage classesPage = new AgentClassesPage(_window);
            _window.Content = classesPage;

            _window.EndWaitMouse();
        }

        private void GameLogButton_Click(object sender, RoutedEventArgs e)
        {
            _window.WaitMouse();
        
            GameLogPage gamesPage = new GameLogPage(_window);
            _window.Content = gamesPage;

            _window.EndWaitMouse();
        }
    }
}

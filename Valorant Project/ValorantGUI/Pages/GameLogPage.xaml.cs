using BussinessLayer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for GameLogPage.xaml
    /// </summary>
    public partial class GameLogPage : Page
    {
        internal GameLogManager GameManager { get; private set; }
        MainWindow _window;

        public GameLogPage(MainWindow window)
        {
            InitializeComponent();
            GameManager = new GameLogManager();
            _window = window;
            PopulateGames();
        }

        internal void PopulateGames()
        {
            ClearAllUi();

            GamesListBox.ItemsSource = GameManager.GetAllEntries();
            TotalKDStatLabel.Content = Math.Round(GameManager.GetTotalKD(), 3).ToString();
            TotalWinLossStatLabel.Content = Math.Round(GameManager.GetTotalWinLoss(), 3).ToString();
            TotalKillsLossStatLabel.Content = GameManager.GetTotals(GameLogManager.Fields.Kills).ToString();
            TotalDeathsLossStatLabel.Content = GameManager.GetTotals(GameLogManager.Fields.Deaths).ToString();
            BestMapStatLabel.Content = GameManager.GetMapWithMostWins().ToString();
            FavAgentStatLabel.Content = GameManager.GetMostPlayedAgent().ToString();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _window.SetHomePage();
        }

        private void ClearAllUi()
        {
            GamesListBox.ItemsSource = null;
            GamesListBox.SelectedIndex = -1;
            ResultLabel.Content = "Result: -";
            ScoreLabel.Content = "Score: -";
            KillsLabel.Content = "Kills: -";
            DeathsLabel.Content = "Deaths: -";
            AssistsLabel.Content = "Assists: -";
            AdrLabel.Content = "ADR: -";
            MapLabel.Content = "Map: -";
            AgentLabel.Content = "Agent: -";

        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GamesListBox.SelectedIndex >= 0)
            {
                int teamScore = int.Parse(GameManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.TeamScore));
                int opponentScore = int.Parse(GameManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.OpponentScore));
                int kills = int.Parse(GameManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.Kills));
                int deaths = int.Parse(GameManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.Deaths));

                ResultLabel.Content = $"Result: {(teamScore > opponentScore ? "Win" : "Loss")}";
                ScoreLabel.Content = $"Score: {teamScore}:{opponentScore}";
                KillsLabel.Content = $"Kills: {kills}";
                DeathsLabel.Content = $"Deaths: {deaths}";
                AssistsLabel.Content = $"Assists: {GameManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.Assists)}";
                AdrLabel.Content = $"ADR: {GameManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.ADR)}";
                KDLabel.Content = $"K/D: {Math.Round((float)kills / (float)deaths, 3)}";

                MapLabel.Content = $"Map: {GameManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.Map)}";
                AgentLabel.Content = $"Agent: {GameManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.Agent)}";

                MapImage.Source = new BitmapImage(new Uri(new MapManager().GetMapsDataStr(GameManager.GetGameMapObj(GamesListBox.SelectedItem), MapManager.Fields.ImagePath), UriKind.Relative));
                AgentImage.Source = new BitmapImage(new Uri(new AgentManager().GetAgentDataStr(GameManager.GetGameAgentObj(GamesListBox.SelectedItem), AgentManager.Fields.ImagePath), UriKind.Relative));
            }
        }

        private void LogGameButton_Click(object sender, RoutedEventArgs e)
        {
            AddGameLogWindow addGameWindow = new AddGameLogWindow(this);
            addGameWindow.Show();
        }

        private void EditGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesListBox.SelectedIndex >= 0)
            {
                EditGameLog editGameLog = new EditGameLog(this, GamesListBox.SelectedItem);
                editGameLog.Show();
            }
            else
            {
                MessageBox.Show("You need to select an game to edit first");
            }
        }

        private void RemoveGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesListBox.SelectedIndex >= 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure out want to delete {GamesListBox.SelectedItem}?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    GameManager.RemoveEntry(GamesListBox.SelectedItem);
                    PopulateGames();
                }
            }
            else
            {
                MessageBox.Show("You need to select an game to remove first");
            }
        }
    }
}

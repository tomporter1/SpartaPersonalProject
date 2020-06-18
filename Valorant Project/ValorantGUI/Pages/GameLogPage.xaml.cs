using BussinessLayer;
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

            GamesListBox.ItemsSource = GameManager.GetAllGames();
            TotalKDLabel.Content = $"Overall K/D: - {GameManager.GetTotalKD()}";
            TotalWinLossLabel.Content = $"Overall Win Loss: - {GameManager.GetTotalWinLoss()}";
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
            AgentLabel.Content = "Agent: ";

        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GamesListBox.SelectedIndex >= 0)
            {
                int teamScore = int.Parse(GameManager.GetGameData(GamesListBox.SelectedItem, GameLogManager.Fields.TeamScore));
                int opponentScore = int.Parse(GameManager.GetGameData(GamesListBox.SelectedItem, GameLogManager.Fields.OpponentScore));
                int kills = int.Parse(GameManager.GetGameData(GamesListBox.SelectedItem, GameLogManager.Fields.Kills));
                int deaths = int.Parse(GameManager.GetGameData(GamesListBox.SelectedItem, GameLogManager.Fields.Deaths));

                ResultLabel.Content = $"Result: {(teamScore > opponentScore ? "Win" : "Loss")}";
                ScoreLabel.Content = $"Score: {teamScore} : {opponentScore}";
                KillsLabel.Content = $"Kills: {kills}";
                DeathsLabel.Content = $"Deaths: {deaths}";
                AssistsLabel.Content = $"Assists: {GameManager.GetGameData(GamesListBox.SelectedItem, GameLogManager.Fields.Assists)}";
                AdrLabel.Content = $"ADR: {GameManager.GetGameData(GamesListBox.SelectedItem, GameLogManager.Fields.ADR)}";
                KDLabel.Content = $"K/D: {((float)kills) / ((float)deaths)}";

                MapLabel.Content = $"Map: {GameManager.GetGameData(GamesListBox.SelectedItem, GameLogManager.Fields.Map)}";
                AgentLabel.Content = $"Agent: {GameManager.GetGameData(GamesListBox.SelectedItem, GameLogManager.Fields.Agent)}";
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
                MessageBox.Show("You need to select an agent to remove first");
            }
        }

        private void RemoveGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesListBox.SelectedIndex >= 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure out want to delete {GamesListBox.SelectedItem}?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    GameManager.RemoveGame(GamesListBox.SelectedItem);
                    PopulateGames();
                }
            }
            else
            {
                MessageBox.Show("You need to select an agent to remove first");
            }
        }
    }
}

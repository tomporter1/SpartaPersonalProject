using BussinessLayer;
using BussinessLayer.Interfaces;
using BussinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ValorantGUI.Windows.GameLogs;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for GameLogPage.xaml
    /// </summary>
    public partial class GameLogPage : Page, IPage
    {
        private IGameLogManager _gameLogManager;
        private IStats _statsManager;
        private MainWindow _window;

        public GameLogPage(MainWindow window, IGameLogManager gameLogManager, GameModesManager modeManager, IStats statsManager)
        {
            InitializeComponent();
            _gameLogManager = gameLogManager;
            _window = window;
            _statsManager = statsManager;

            GameModeComboBox.ItemsSource = modeManager.GetAllEntries();
            GameModeComboBox.SelectedIndex = 0;

            List<string> seasonSelections = new List<string>();
            seasonSelections.Add("All");
            for (int i = 1; i <= _gameLogManager.CurrentSeasonNum; i++)
            {
                seasonSelections.Add(i.ToString());
            }
            SeasonComboBox.ItemsSource = seasonSelections;
            SeasonComboBox.SelectedIndex = 0;

            PopulateItems();
        }

        internal void SetNewSeasonNum(int newSeasonNum) => _gameLogManager.CurrentSeasonNum = newSeasonNum;

        //internal int GetCurrentSeason() => GameManager.CurrentSeasonNum;

        public void PopulateItems()
        {
            string season = SeasonComboBox.SelectedItem.ToString();
            ClearAllUi();

            GamesListBox.ItemsSource = _gameLogManager.GetGamesForGameMode(GameModeComboBox.SelectedItem, season);

            TotalKDStatLabel.Content = Math.Round(_statsManager.GetTotalKD(GameModeComboBox.SelectedItem, season), 3).ToString();
            TotalWinLossStatLabel.Content = Math.Round(_statsManager.GetTotalWinLoss(GameModeComboBox.SelectedItem, season), 3).ToString();
            TotalKillsLossStatLabel.Content = _statsManager.GetTotals(GameLogManager.Fields.Kills, GameModeComboBox.SelectedItem, season).ToString();
            TotalDeathsLossStatLabel.Content = _statsManager.GetTotals(GameLogManager.Fields.Deaths, GameModeComboBox.SelectedItem, season).ToString();

            object mapMostWins = _statsManager.GetMapWithMostWins(GameModeComboBox.SelectedItem, season);
            BestMapStatLabel.Content = mapMostWins == null ? "-" : mapMostWins.ToString();

            object MostPlayedAgent = _statsManager.GetMostPlayedAgent(GameModeComboBox.SelectedItem, season);
            FavAgentStatLabel.Content = MostPlayedAgent == null ? "-" : MostPlayedAgent.ToString();

            object mostPlayedClass = _statsManager.GetMostPlayedClass(GameModeComboBox.SelectedItem, season);
            FavTypeStatLabel.Content = mostPlayedClass == null ? "-" : mostPlayedClass.ToString();
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
            KDLabel.Content = "K/D: -";
            MapLabel.Content = "Map: -";
            AgentLabel.Content = "Agent: -";
            DateLabel.Content = "Date Logged: -";

            AgentImage.Source = null;
            MapImage.Source = null;
            RankImage.Source = null;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GamesListBox.SelectedIndex >= 0)
            {
                int teamScore = int.Parse(_gameLogManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.TeamScore));
                int opponentScore = int.Parse(_gameLogManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.OpponentScore));
                int kills = int.Parse(_gameLogManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.Kills));
                int deaths = int.Parse(_gameLogManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.Deaths));

                ResultLabel.Content = $"Result: {(teamScore > opponentScore ? "Win" : (teamScore < opponentScore ? "Loss" : "Draw"))}";
                ScoreLabel.Content = $"Score: {teamScore}:{opponentScore}";
                KillsLabel.Content = $"Kills: {kills}";
                DeathsLabel.Content = $"Deaths: {deaths}";
                AssistsLabel.Content = $"Assists: {_gameLogManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.Assists)}";
                AdrLabel.Content = $"ADR: {_gameLogManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.ADR)}";
                KDLabel.Content = $"K/D: {Math.Round((float)kills / (float)deaths, 3)}";

                MapLabel.Content = $"Map: {_gameLogManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.Map)}";
                AgentLabel.Content = $"Agent: {_gameLogManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.Agent)}";

                DateLabel.Content = $"Date Logged: {_gameLogManager.GetGameDataStr(GamesListBox.SelectedItem, GameLogManager.Fields.DateLogged)}";
                string mapImagePath = new MapManager().GetMapsDataStr(_gameLogManager.GetGameMapObj(GamesListBox.SelectedItem), MapManager.Fields.ImagePath);
                if (mapImagePath != null && mapImagePath != "")
                    MapImage.Source = new BitmapImage(new Uri(mapImagePath, UriKind.Relative));
                else
                    MapImage.Source = null;

                string agentImagePath = new AgentManager().GetAgentDataStr(_gameLogManager.GetGameAgentObj(GamesListBox.SelectedItem), AgentManager.Fields.ImagePath);
                if (agentImagePath != null && agentImagePath != "")
                    AgentImage.Source = new BitmapImage(new Uri(agentImagePath, UriKind.Relative));
                else
                    AgentImage.Source = null;

                string rankImagePath = new RankManager().GetRankDataStr(_gameLogManager.GetGameRankObj(GamesListBox.SelectedItem), RankManager.Fields.ImagePath);
                if (rankImagePath != null && rankImagePath != "")
                    RankImage.Source = new BitmapImage(new Uri(rankImagePath, UriKind.Relative));
                else
                    RankImage.Source = null;
            }
        }

        private void LogGameButton_Click(object sender, RoutedEventArgs e)
        {
            AddGameLogWindow addGameWindow = new AddGameLogWindow(this, _gameLogManager);
            addGameWindow.Show();
        }

        private void EditGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesListBox.SelectedIndex >= 0)
            {
                EditGameLog editGameLog = new EditGameLog(this, GamesListBox.SelectedItem, _gameLogManager);
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
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure out want to delete \"{GamesListBox.SelectedItem}\"?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _gameLogManager.RemoveEntry(GamesListBox.SelectedItem);
                    PopulateItems();
                }
            }
            else
            {
                MessageBox.Show("You need to select an game to remove first");
            }
        }

        private void OnGameModeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SeasonComboBox.SelectedIndex >= 0)
                PopulateItems();
        }

        private void OnSeasonSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SeasonComboBox.SelectedIndex >= 0)
                PopulateItems();
        }

        private void SetSeason_Click(object sender, RoutedEventArgs e)
        {
            SetSeason newSeasonWindow = new SetSeason(this);
            newSeasonWindow.Show();
        }

        private void NextSeason_Click(object sender, RoutedEventArgs e)
        {
            _gameLogManager.CurrentSeasonNum++;
            MessageBox.Show($"We are now in season {_gameLogManager.CurrentSeasonNum}");
        }

        private void FindMostKillsGamme_Click(object sender, RoutedEventArgs e)
        {
            object mostKillsGame = _statsManager.GetMostKillsGame(SeasonComboBox.SelectedItem.ToString(), GameModeComboBox.SelectedItem);
            foreach (object game in GamesListBox.Items)
            {
                if (mostKillsGame.Equals(game))
                {
                    GamesListBox.SelectedItem = game;
                }
            }
        }

        private void FindMostKDGamme_Click(object sender, RoutedEventArgs e)
        {
            object mostKDGame = _statsManager.GetMostKDGame(SeasonComboBox.SelectedItem.ToString(), GameModeComboBox.SelectedItem);
            foreach (object game in GamesListBox.Items)
            {
                if (mostKDGame.Equals(game))
                {
                    GamesListBox.SelectedItem = game;
                }
            }
        }
    }
}
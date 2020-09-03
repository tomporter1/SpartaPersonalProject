using BussinessLayer.Interfaces;
using BussinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ValorantGUI.Windows.GameLogs;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for GameLogPage.xaml
    /// </summary>
    public partial class GameLogPage : Page, IPage
    {
        private readonly IGameLogManager _gameLogManager;
        private readonly IModeManager _modeManager;
        private readonly IAgentManager _agentManager;
        private readonly IStats _statsManager;
        private readonly IMapManager _mapManager;
        private readonly IRanksManger _rankManager;
        private readonly IRankAdjustmentManager _rankAdjustmentManager;
        private readonly IWindow _window;

        public GameLogPage(IWindow window, IGameLogManager gameLogManager, IAgentManager agentManager, IStats statsManager, IMapManager mapManager, IRanksManger ranksManger, IModeManager modeManager, IRankAdjustmentManager adjustmentManager)
        {
            InitializeComponent();
            _gameLogManager = gameLogManager;
            _modeManager = modeManager;
            _agentManager = agentManager;
            _window = window;
            _statsManager = statsManager;
            _mapManager = mapManager;
            _rankManager = ranksManger;
            _rankAdjustmentManager = adjustmentManager;

            GameModeComboBox.ItemsSource = modeManager.GetAllEntries();
            GameModeComboBox.SelectedIndex = 0;

            List<string> seasonSelections = new List<string> { "All" };
            for (int i = 1; i <= _gameLogManager.CurrentSeasonNum; i++)
            {
                seasonSelections.Add(i.ToString());
            }
            SeasonComboBox.ItemsSource = seasonSelections;
            SeasonComboBox.SelectedIndex = 0;

            PopulateItems();
        }

        public void PopulateItems()
        {
            string season = SeasonComboBox.SelectedItem.ToString();
            ClearAllUi();

            foreach (object game in _gameLogManager.GetGamesForGameMode(GameModeComboBox.SelectedItem, season))
            {
                GamesListBox.Items.Add(new CustomBackgroundItem(game, GetGameColor(game)));
            }

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

        private Color GetGameColor(object game)
        {
            GameLogManager.Results gameOutcome = _gameLogManager.GetMatchResult(game);


            switch (gameOutcome)
            {
                case GameLogManager.Results.Win:
                    return new Color() { R = 119, G = 193, B = 172, A = 100 };
                case GameLogManager.Results.Loss:
                    return new Color() { R = 230, G = 100, B = 95, A = 100 };
                case GameLogManager.Results.Draw:
                    return new Color() { R = 168, G = 168, B = 169, A = 100 };
                default:
                    return new Color();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _window.SetHomePage();
        }

        private void ClearAllUi()
        {
            GamesListBox.Items.Clear();
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
            RankAdjustmentImage.Source = null;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GamesListBox.SelectedIndex >= 0)
            {
                ResultLabel.Content = $"Result: {_gameLogManager.GetGameDataStr(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj, GameLogManager.Fields.Result)}";
                ScoreLabel.Content = $"Score: {_gameLogManager.GetGameDataStr(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj, GameLogManager.Fields.Score)}";

                KillsLabel.Content = $"Kills: {_gameLogManager.GetGameDataStr(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj, GameLogManager.Fields.Kills)}";
                DeathsLabel.Content = $"Deaths: {_gameLogManager.GetGameDataStr(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj, GameLogManager.Fields.Deaths)}";

                AssistsLabel.Content = $"Assists: {_gameLogManager.GetGameDataStr(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj, GameLogManager.Fields.Assists)}";
                AdrLabel.Content = $"ADR: {_gameLogManager.GetGameDataStr(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj, GameLogManager.Fields.ADR)}";

                KDLabel.Content = $"K/D: {_gameLogManager.GetGameDataStr(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj, GameLogManager.Fields.KD)}";

                MapLabel.Content = $"Map: {_gameLogManager.GetGameDataStr(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj, GameLogManager.Fields.Map)}";
                AgentLabel.Content = $"Agent: {_gameLogManager.GetGameDataStr(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj, GameLogManager.Fields.Agent)}";

                DateLabel.Content = $"Date Logged: {_gameLogManager.GetGameDataStr(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj, GameLogManager.Fields.DateLogged)}";

                string mapImagePath = _mapManager.GetMapsDataStr(_gameLogManager.GetGameMapObj(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj), MapManager.Fields.ImagePath);
                SetImageSource(MapImage, mapImagePath);

                string agentImagePath = _agentManager.GetAgentDataStr(_gameLogManager.GetGameAgentObj(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj), AgentManager.Fields.ImagePath);
                SetImageSource(AgentImage, agentImagePath);

                string rankImagePath = _rankManager.GetRankDataStr(_gameLogManager.GetGameRankObj(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj), RankManager.Fields.ImagePath);
                SetImageSource(RankImage, rankImagePath);

                string rankAdjustmentImagePath = _rankAdjustmentManager.GetRankAdjustmentDataStr(_gameLogManager.GetRankAdjustmentObj(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj), RankAdjustmentManager.Fields.ImagePath);
                SetImageSource(RankAdjustmentImage, rankAdjustmentImagePath);
            }
        }

        internal static void SetImageSource(Image image, string path) => image.Source = path != null && path != "" ? new BitmapImage(new Uri(path, UriKind.Relative)) : null;

        private void LogGameButton_Click(object sender, RoutedEventArgs e)
        {
            AddGameLogWindow addGameWindow = new AddGameLogWindow(this, _gameLogManager, _modeManager, _mapManager, _agentManager, _rankManager, _rankAdjustmentManager);
            addGameWindow.Show();
        }

        private void EditGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesListBox.SelectedIndex >= 0)
            {
                EditGameLog editGameLog = new EditGameLog(this, ((CustomBackgroundItem)GamesListBox.SelectedItem).Obj, _gameLogManager, _modeManager, _rankManager, _rankAdjustmentManager);
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
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure out want to delete \"{((CustomBackgroundItem)GamesListBox.SelectedItem).Obj}\"?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _gameLogManager.RemoveEntry(((CustomBackgroundItem)GamesListBox.SelectedItem).Obj);
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
            SetSeason newSeasonWindow = new SetSeason(_gameLogManager);
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
                if (mostKillsGame.Equals(((CustomBackgroundItem)game).Obj))
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
                if (mostKDGame.Equals(((CustomBackgroundItem)game).Obj))
                {
                    GamesListBox.SelectedItem = game;
                }
            }
        }
    }
}
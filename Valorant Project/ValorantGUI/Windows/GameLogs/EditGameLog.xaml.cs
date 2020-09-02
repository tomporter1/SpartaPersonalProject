using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using BussinessLayer.Managers;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for EditGameLog.xaml
    /// </summary>
    public partial class EditGameLog : Window
    {
        private object _selectedGame;
        private DateTime _oldTime;
        private readonly IModeManager _modesManager;
        private readonly IGameLogManager _logManager;
        private readonly IPage _gameLogPage;

        public EditGameLog(IPage gameLogPage, object selectedGame, IGameLogManager logManager, IModeManager modeManager, IRanksManger ranksManger, IRankAdjustmentManager adjustmentManager)
        {
            InitializeComponent();
            _gameLogPage = gameLogPage;
            _selectedGame = selectedGame;
            _logManager = logManager;
            _modesManager = modeManager;

            TeamScoreTextBox.Text = _logManager.GetGameDataStr(selectedGame, GameLogManager.Fields.TeamScore);
            OpponentScoreTextBox.Text = _logManager.GetGameDataStr(selectedGame, GameLogManager.Fields.OpponentScore);
            KillsTextBox.Text = _logManager.GetGameDataStr(selectedGame, GameLogManager.Fields.Kills);
            AssistsTextBox.Text = _logManager.GetGameDataStr(selectedGame, GameLogManager.Fields.Assists);
            DeathsTextBox.Text = _logManager.GetGameDataStr(selectedGame, GameLogManager.Fields.Deaths);
            ADRTextBox.Text = _logManager.GetGameDataStr(selectedGame, GameLogManager.Fields.ADR);

            _oldTime = _logManager.GetDatePlayed(selectedGame);
            DatePlayedPicker.SelectedDate = _oldTime;

            MapComboBox.ItemsSource = new MapManager().GetAllEntries();
            object gameMap = _logManager.GetGameMapObj(selectedGame);
            foreach (var item in MapComboBox.ItemsSource)
            {
                if (item.Equals(gameMap))
                {
                    MapComboBox.SelectedItem = item;
                }
            }

            AgentComboBox.ItemsSource = new AgentManager().GetAllEntries();
            object gameAgent = _logManager.GetGameAgentObj(selectedGame);
            foreach (object item in AgentComboBox.ItemsSource)
            {
                if (item.Equals(gameAgent))
                {
                    AgentComboBox.SelectedItem = item;
                }
            }

            ModeComboBox.ItemsSource = modeManager.GetAllEntries();
            object gameMode = _logManager.GetGameMode(selectedGame);
            foreach (object item in ModeComboBox.ItemsSource)
            {
                if (item.Equals(gameMode))
                {
                    ModeComboBox.SelectedItem = item;
                }
            }

            RankComboBox.ItemsSource = ranksManger.GetAllEntries();
            RankAdjustmentComboBox.ItemsSource = adjustmentManager.GetAllEntries();
            if (_modesManager.IsRanked(ModeComboBox.SelectedItem))
            {
                object rank = _logManager.GetRankObj(selectedGame);
                foreach (object item in RankComboBox.ItemsSource)
                {
                    if (item.Equals(rank))
                    {
                        RankComboBox.SelectedItem = item;
                    }
                }

                object rankAdjustment = _logManager.GetRankAdjustmentObj(selectedGame);
                foreach (object item in RankAdjustmentComboBox.ItemsSource)
                {
                    if (item.Equals(rankAdjustment))
                    {
                        RankAdjustmentComboBox.SelectedItem = item;
                    }
                }
            }
            else
            {
                RankComboBox.IsEnabled = false;
                RankAdjustmentComboBox.IsEnabled = false;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamScoreTextBox.Text.Trim() != "" && OpponentScoreTextBox.Text.Trim() != "" && MapComboBox.SelectedIndex >= 0 && AgentComboBox.SelectedIndex >= 0 && ModeComboBox.SelectedIndex >= 0)
            {
                GameLogArgs args = new GameLogArgs(
                    ModeComboBox.SelectedItem,
                    MapComboBox.SelectedItem,
                    AgentComboBox.SelectedItem,
                    int.Parse(TeamScoreTextBox.Text.Trim()),
                    int.Parse(OpponentScoreTextBox.Text.Trim()),
                    int.Parse(KillsTextBox.Text.Trim()),
                    int.Parse(DeathsTextBox.Text.Trim()),
                    int.Parse(AssistsTextBox.Text.Trim()),
                    float.Parse(ADRTextBox.Text.Trim()),
                    ((DateTime)DatePlayedPicker.SelectedDate).AddHours(_oldTime.Hour).AddMinutes(_oldTime.Minute).AddSeconds(_oldTime.Second),
                    _logManager.CurrentSeasonNum,
                    _modesManager.IsRanked(ModeComboBox.SelectedItem) ? RankComboBox.SelectedItem : null,
                    _modesManager.IsRanked(ModeComboBox.SelectedItem) ? RankAdjustmentComboBox.SelectedItem : null);
                _logManager.UpdateEntry(_selectedGame, args);
                _gameLogPage.PopulateItems();
                this.Close();
            }
            else
            {
                if (_modesManager.IsRanked(ModeComboBox.SelectedItem) && RankComboBox.SelectedIndex >= 0)
                {
                    RankLabel.Foreground = Brushes.Red;

                    MessageBox.Show("Please select a rank for this match");
                }

                if (TeamScoreTextBox.Text.Trim() == "")
                    teamScoreLabel.Foreground = Brushes.Red;
                if (OpponentScoreTextBox.Text.Trim() == "")
                    opponentScoreLabel.Foreground = Brushes.Red;
                if (MapComboBox.SelectedIndex < 0)
                    MapLabel.Foreground = Brushes.Red;
                if (AgentComboBox.SelectedIndex < 0)
                    AgentLabel.Foreground = Brushes.Red;
                if (ModeComboBox.SelectedIndex < 0)
                    ModeLabel.Foreground = Brushes.Red;

                MessageBox.Show("Please fill in the required fields");
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ModeSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_modesManager.IsRanked(ModeComboBox.SelectedItem))
            {
                RankComboBox.IsEnabled = true;
                RankAdjustmentComboBox.IsEnabled = true;
            }
            else
            {
                RankComboBox.IsEnabled = false;
                RankAdjustmentComboBox.IsEnabled = false;
            }
        }
    }
}
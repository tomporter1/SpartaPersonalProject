using BussinessLayer;
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
        private GameLogPage _gameLogPage;
        private object _selectedGame;
        private DateTime _oldTime;

        public EditGameLog(GameLogPage gameLogPage, object selectedGame)
        {
            InitializeComponent();
            _gameLogPage = gameLogPage;
            _selectedGame = selectedGame;

            TeamScoreTextBox.Text = _gameLogPage.GameManager.GetGameDataStr(selectedGame, GameLogManager.Fields.TeamScore);
            OpponentScoreTextBox.Text = _gameLogPage.GameManager.GetGameDataStr(selectedGame, GameLogManager.Fields.OpponentScore);
            KillsTextBox.Text = _gameLogPage.GameManager.GetGameDataStr(selectedGame, GameLogManager.Fields.Kills);
            AssistsTextBox.Text = _gameLogPage.GameManager.GetGameDataStr(selectedGame, GameLogManager.Fields.Assists);
            DeathsTextBox.Text = _gameLogPage.GameManager.GetGameDataStr(selectedGame, GameLogManager.Fields.Deaths);
            ADRTextBox.Text = _gameLogPage.GameManager.GetGameDataStr(selectedGame, GameLogManager.Fields.ADR);

            _oldTime = _gameLogPage.GameManager.GetDatePlayed(selectedGame);
            DatePlayedPicker.SelectedDate = _oldTime;

            MapComboBox.ItemsSource = new MapManager().GetAllEntries();
            object gameMap = _gameLogPage.GameManager.GetGameMapObj(selectedGame);
            foreach (var item in MapComboBox.ItemsSource)
            {
                if (item.Equals(gameMap))
                {
                    MapComboBox.SelectedItem = item;
                }
            }

            AgentComboBox.ItemsSource = new AgentManager().GetAllEntries();
            object gameAgent = _gameLogPage.GameManager.GetGameAgentObj(selectedGame);
            foreach (object item in AgentComboBox.ItemsSource)
            {
                if (item.Equals(gameAgent))
                {
                    AgentComboBox.SelectedItem = item;
                }
            }

            ModeComboBox.ItemsSource = new GameModesManager().GetAllEntries();
            object gameMode = _gameLogPage.GameManager.GetGameMode(selectedGame);
            foreach (object item in ModeComboBox.ItemsSource)
            {
                if (item.Equals(gameMode))
                {
                    ModeComboBox.SelectedItem = item;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamScoreTextBox.Text.Trim() != "" && OpponentScoreTextBox.Text.Trim() != "" && MapComboBox.SelectedIndex >= 0 && AgentComboBox.SelectedIndex >= 0 && ModeComboBox.SelectedIndex >=0)
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
                    _gameLogPage.GetCurrentSeason());
                _gameLogPage.GameManager.UpdateEntry(_selectedGame, args);
                _gameLogPage.PopulateGames(_gameLogPage.SeasonComboBox.SelectedItem.ToString());
                this.Close();
            }
            else
            {
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
    }
}
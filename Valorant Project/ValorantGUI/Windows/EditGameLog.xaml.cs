using BussinessLayer;
using System;
using System.Windows;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for EditGameLog.xaml
    /// </summary>
    public partial class EditGameLog : Window
    {
        GameLogPage _gameLogPage;
        object _selectedGame;
        DateTime oldTime;
        public EditGameLog(GameLogPage gameLogPage, object selectedGame)
        {
            InitializeComponent();
            _gameLogPage = gameLogPage;
            _selectedGame = selectedGame;

            TeamScoreTextBox.Text = _gameLogPage.GameManager.GetGameData(selectedGame, GameLogManager.Fields.TeamScore);
            OpponentScoreTextBox.Text = _gameLogPage.GameManager.GetGameData(selectedGame, GameLogManager.Fields.OpponentScore);
            KillsTextBox.Text = _gameLogPage.GameManager.GetGameData(selectedGame, GameLogManager.Fields.Kills);
            AssistsTextBox.Text = _gameLogPage.GameManager.GetGameData(selectedGame, GameLogManager.Fields.Assists);
            DeathsTextBox.Text = _gameLogPage.GameManager.GetGameData(selectedGame, GameLogManager.Fields.Deaths);
            ADRTextBox.Text = _gameLogPage.GameManager.GetGameData(selectedGame, GameLogManager.Fields.ADR);

            oldTime = _gameLogPage.GameManager.GetDatePlayed(selectedGame);
            DatePlayedPicker.SelectedDate = oldTime;

            MapComboBox.ItemsSource = new MapManager().GetAllMaps();
            object gameMap = _gameLogPage.GameManager.GetGameMap(selectedGame);
            foreach (var item in MapComboBox.ItemsSource)
            {
                if (item.Equals(gameMap))
                {
                    MapComboBox.SelectedItem = item;
                }
            }

            AgentComboBox.ItemsSource = new AgentManager().GetAllAgents();
            object gameAgent = _gameLogPage.GameManager.GetGameAgent(selectedGame);
            foreach (object item in AgentComboBox.ItemsSource)
            {
                if (item.Equals(gameAgent))
                {
                    AgentComboBox.SelectedItem = item;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogArgs args = new GameLogArgs(
                MapComboBox.SelectedItem,
                AgentComboBox.SelectedItem,
                int.Parse(TeamScoreTextBox.Text.Trim()),
                int.Parse(OpponentScoreTextBox.Text.Trim()),
                int.Parse(KillsTextBox.Text.Trim()),
                int.Parse(DeathsTextBox.Text.Trim()),
                int.Parse(AssistsTextBox.Text.Trim()),
                int.Parse(ADRTextBox.Text.Trim()),
                ((DateTime)DatePlayedPicker.SelectedDate).AddHours(oldTime.Hour).AddMinutes(oldTime.Minute).AddSeconds(oldTime.Second));
            _gameLogPage.GameManager.UpdateGame(_selectedGame, args);
            _gameLogPage.PopulateGames();
            this.Close();
        }
    }
}
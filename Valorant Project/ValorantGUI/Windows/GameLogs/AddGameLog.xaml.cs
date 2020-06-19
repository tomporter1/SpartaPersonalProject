using BussinessLayer;
using System;
using System.Windows;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AddGameLogWindow.xaml
    /// </summary>
    public partial class AddGameLogWindow : Window
    {
        GameLogPage _gameLogPage;

        public AddGameLogWindow(GameLogPage gameLogPage)
        {
            InitializeComponent();
            _gameLogPage = gameLogPage;

            MapComboBox.ItemsSource = new MapManager().GetAllEntries();
            AgentComboBox.ItemsSource = new AgentManager().GetAllEntries();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            GameLogArgs args = new GameLogArgs(
                MapComboBox.SelectedItem,
                AgentComboBox.SelectedItem,
                int.Parse(TeamScoreTextBox.Text),
                int.Parse(OpponentScoreTextBox.Text),
                int.Parse(KillsTextBox.Text),
                int.Parse(DeathsTextBox.Text),
                int.Parse(AssistsTextBox.Text),
                int.Parse(ADRTextBox.Text),
                DateTime.Now);

            _gameLogPage.GameManager.AddNewGame(args);

            _gameLogPage.PopulateGames();
            this.Close();
        }
    }
}

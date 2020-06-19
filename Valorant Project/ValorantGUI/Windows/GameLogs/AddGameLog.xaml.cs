using BussinessLayer;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

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

            _gameLogPage.GameManager.AddNewEntry(args);

            _gameLogPage.PopulateGames();
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

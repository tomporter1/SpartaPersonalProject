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
using System.Windows.Shapes;

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

            MapComboBox.ItemsSource = new MapManager().GetAllMaps();
            AgentComboBox.ItemsSource = new AgentManager().GetAllAgents();
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

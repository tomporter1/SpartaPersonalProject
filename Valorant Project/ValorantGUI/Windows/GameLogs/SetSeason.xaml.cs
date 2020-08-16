using BussinessLayer.Interfaces;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ValorantGUI.Windows.GameLogs
{
    /// <summary>
    /// Interaction logic for SetSeason.xaml
    /// </summary>
    public partial class SetSeason : Window
    {
        private readonly IGameLogManager _logManager;

        public SetSeason(IGameLogManager logManager)
        {
            _logManager = logManager;
            InitializeComponent();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            _logManager.CurrentSeasonNum = int.Parse(numberInputTextbox.Text);
            Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
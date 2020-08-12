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
        private GameLogPage _logPage;

        public SetSeason(GameLogPage logPage)
        {
            _logPage = logPage;
            InitializeComponent();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            _logPage.SetNewSeasonNum(int.Parse(numberInputTextbox.Text));
            Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
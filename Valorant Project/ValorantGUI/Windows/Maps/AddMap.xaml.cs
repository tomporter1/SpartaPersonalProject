using BussinessLayer;
using System.Windows;
using System.Windows.Media;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AddMap.xaml
    /// </summary>
    public partial class AddMap : Window
    {
        private MapsPage _mapPage;

        public AddMap(MapsPage mapPage)
        {
            InitializeComponent();
            _mapPage = mapPage;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Trim() != "")
            {
                _mapPage.MapManager.AddNewEntry(new MapArgs(NameTextBox.Text.Trim()));
                _mapPage.PopulateMaps();
                this.Close();
            }
            else
            {
                NameLabel.Foreground = Brushes.Red;

                MessageBox.Show("Please fill in the required fields");
            }
        }
    }
}
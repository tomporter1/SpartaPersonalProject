using System.Windows;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AddMap.xaml
    /// </summary>
    public partial class AddMap : Window
    {
        MapsPage _mapPage;
        public AddMap(MapsPage mapPage)
        {
            InitializeComponent();
            _mapPage = mapPage;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _mapPage.MapManager.AddNewMap(NameTextBox.Text.Trim());
            _mapPage.PopulateMaps();
            this.Close();
        }
    }
}

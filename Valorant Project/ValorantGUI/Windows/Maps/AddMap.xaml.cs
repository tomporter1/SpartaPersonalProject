using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using System.Windows;
using System.Windows.Media;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AddMap.xaml
    /// </summary>
    public partial class AddMap : Window
    {
        private readonly IPage _mapPage;
        private readonly IBasicManager _basicMapManager;

        public AddMap(IPage mapPage, IBasicManager basicMapManager)
        {
            InitializeComponent();
            _mapPage = mapPage;
            _basicMapManager = basicMapManager;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Trim() != "")
            {
                _basicMapManager.AddNewEntry(new MapArgs(NameTextBox.Text.Trim()));
                _mapPage.PopulateItems();
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
using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using BussinessLayer.Managers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ValorantGUI
{

    /// <summary>
    /// Interaction logic for MapsPage.xaml
    /// </summary>
    public partial class MapsPage : Page, IPage
    {
        private readonly IWindow _window;
        private readonly IMapManager _mapManager;

        public MapsPage(IWindow window, IMapManager mapManager)
        {
            InitializeComponent();
            _window = window;
            _mapManager = mapManager;

            PopulateItems();
        }

        public void PopulateItems()
        {
            MapsListBox.ItemsSource = null;
            MapsListBox.SelectedIndex = -1;
            NameTextBox.Text = "";
            MapsListBox.ItemsSource = _mapManager.GetAllEntries();
            NameTextBox.IsEnabled = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddMap addMap = new AddMap(this, _mapManager);
            addMap.Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _window.SetHomePage();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MapsListBox.SelectedIndex >= 0)
            {
                NameTextBox.IsEnabled = true;
                NameTextBox.Text = _mapManager.GetMapsDataStr(MapsListBox.SelectedItem, IMapManager.Fields.Name);
                string path = _mapManager.GetMapsDataStr(MapsListBox.SelectedItem, IMapManager.Fields.LayoutImagePath);
                if (path != null && path != "")
                    MapImage.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                else
                    MapImage.Source = null;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (MapsListBox.SelectedIndex >= 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure out want to delete {MapsListBox.SelectedItem}?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _mapManager.RemoveEntry(MapsListBox.SelectedItem);
                    PopulateItems();
                }
            }
            else
            {
                MessageBox.Show("Select a map to remove first.");
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (MapsListBox.SelectedIndex >= 0)
            {
                _mapManager.UpdateEntry(MapsListBox.SelectedItem, new MapArgs(NameTextBox.Text.Trim()));
                PopulateItems();
            }
            else
            {
                MessageBox.Show("Select a map to edit first.");
            }
        }
    }
}
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

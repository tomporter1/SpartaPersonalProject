﻿using BussinessLayer;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for MapsPage.xaml
    /// </summary>
    public partial class MapsPage : Page
    {
        MainWindow _window;
        internal MapManager MapManager { get; private set; }

        public MapsPage(MainWindow window)
        {
            InitializeComponent();
            _window = window;
            MapManager = new MapManager();

            PopulateMaps();
        }

        internal void PopulateMaps()
        {
            MapsListBox.ItemsSource = null;
            MapsListBox.SelectedIndex = -1;
            NameTextBox.Text = "";
            MapsListBox.ItemsSource = MapManager.GetAllMaps();
            NameTextBox.IsEnabled = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddMap addMap = new AddMap(this);
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
                NameTextBox.Text = MapManager.GetMapsData(MapsListBox.SelectedItem, MapManager.Fields.Name);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (MapsListBox.SelectedIndex >= 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure out want to delete {MapsListBox.SelectedItem}?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    MapManager.RemoveMap(MapsListBox.SelectedItem);
                    PopulateMaps();
                }
            }
            else
            {
                MessageBox.Show("Select a class to remove first.");
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (MapsListBox.SelectedIndex >= 0)
            {
                MapManager.UpdateMap(MapsListBox.SelectedItem, NameTextBox.Text.Trim());
                PopulateMaps();
            }
            else
            {
                MessageBox.Show("Select a class to edit first.");
            }
        }

    }
}

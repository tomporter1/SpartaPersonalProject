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
    /// Interaction logic for AgentClassesPage.xaml
    /// </summary>
    public partial class AgentClassesPage : Page, IPage
    {
        private IWindow _window;
        private readonly IAgentTypesManager _agentTypeManager;

        public AgentClassesPage(IWindow window, IAgentTypesManager agentTypeManager)
        {
            InitializeComponent();
            _agentTypeManager = agentTypeManager;
            PopulateItems();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TypesListBox.SelectedIndex >= 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure out want to delete {TypesListBox.SelectedItem}?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _agentTypeManager.RemoveEntry(TypesListBox.SelectedItem);
                    PopulateItems();
                }
            }
            else
            {
                MessageBox.Show("Select a class to remove first.");
            }
        }

        public void PopulateItems()
        {
            TypesListBox.ItemsSource = null;
            TypesListBox.SelectedIndex = -1;
            NameTextBox.Text = "";
            TypesListBox.ItemsSource = _agentTypeManager.GetAllEntries();
            NameTextBox.IsEnabled = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddAgentType addAgentType = new AddAgentType(this, _agentTypeManager);
            addAgentType.Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _window.SetHomePage();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypesListBox.SelectedIndex >= 0)
            {
                NameTextBox.Text = _agentTypeManager.GetTypeDataStr(TypesListBox.SelectedItem, IAgentTypesManager.Fields.Name);
                NameTextBox.IsEnabled = true;

                string path = _agentTypeManager.GetTypeDataStr(TypesListBox.SelectedItem, IAgentTypesManager.Fields.ImagePath);
                if (path != null && path != "")
                    TypeImage.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                else
                    TypeImage.Source = null;
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (TypesListBox.SelectedIndex >= 0)
            {
                _agentTypeManager.UpdateEntry(TypesListBox.SelectedItem, new AgentTypeArgs(NameTextBox.Text.Trim()));
                PopulateItems();
            }
            else
            {
                MessageBox.Show("Select a class to edit first.");
            }
        }
    }
}
using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ValorantDatabase;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AgentsPage.xaml
    /// </summary>
    public partial class AgentsPage : Page
    {
        MainWindow _window;
        internal AgentManager AgentManager { get; private set; }

        public AgentsPage(MainWindow window)
        {
            InitializeComponent();
            _window = window;
            AgentManager = new AgentManager();

            PopulateAgents();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _window.SetHomePage();
        }

        internal void PopulateAgents()
        {
            ClearAllUi();
            List<object> allAgents = AgentManager.GetAllEntries();
            if (allAgents.Count != 0)
            {
                AgentNameListBox.ItemsSource = allAgents;
                ClearAbilitiesListBox();
            }
        }

        private void ClearAllUi()
        {
            AgentNameListBox.ItemsSource = null;
            AgentNameListBox.SelectedIndex = -1;

            ClearAbilitiesListBox();

            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            BioTextBox.Text = "";
            AbilityDiscriptionTextBox.Text = "";
            AgentClassLabel.Content = "Agent Class: -";
        }

        private void OnAgentSelected(object sender, SelectionChangedEventArgs e)
        {
            if (AgentNameListBox.SelectedIndex >= 0)
            {
                ClearAbilitiesListBox();
                AbilitiesListBox.ItemsSource = AgentManager.GetAgentsAbilities(AgentNameListBox.SelectedItem);

                ClearTextBoxes();
                BioTextBox.Text = AgentManager.GetAgentDataStr(AgentNameListBox.SelectedItem, AgentManager.Fields.Bio);
                AgentClassLabel.Content = $"Agent Class: {AgentManager.GetAgentDataStr(AgentNameListBox.SelectedItem, AgentManager.Fields.Type)}";

                AgentImage.Source = new BitmapImage(new Uri(AgentManager.GetAgentDataStr(AgentNameListBox.SelectedItem, AgentManager.Fields.ImagePath), UriKind.Relative));
            }
        }

        private void ClearAbilitiesListBox()
        {
            AbilitiesListBox.ItemsSource = null;
            AbilitiesListBox.SelectedIndex = -1;
        }

        private void OnAbilitySelect(object sender, SelectionChangedEventArgs e)
        {
            if (AbilitiesListBox.SelectedIndex >= 0)
            {
                AbilityDiscriptionTextBox.Text = AgentManager.GetAbilityDiscription(AgentNameListBox.SelectedItem, AbilitiesListBox.SelectedItem);
            }
        }

        private void AddNewAgent(object sender, RoutedEventArgs e)
        {
            AddAgent addAgentWindow = new AddAgent(this);
            addAgentWindow.Show();
        }

        private void EditAgentButton_Click(object sender, RoutedEventArgs e)
        {
            if (AgentNameListBox.SelectedIndex >= 0)
            {
                EditAgent editAgent = new EditAgent(AgentNameListBox.SelectedItem, this);
                editAgent.Show();
            }
            else
            {
                MessageBox.Show("You need to select an agent to edit first");
            }
        }

        private void RemoveAgentButton_Click(object sender, RoutedEventArgs e)
        {
            if (AgentNameListBox.SelectedIndex >= 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure out want to delete {AgentNameListBox.SelectedItem}?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    AgentManager.RemoveEntry(AgentNameListBox.SelectedItem);
                    PopulateAgents();
                }
            }
            else
            {
                MessageBox.Show("You need to select an agent to remove first");
            }
        }
    }
}
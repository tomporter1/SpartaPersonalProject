using BussinessLayer.Interfaces;
using BussinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AgentsPage.xaml
    /// </summary>
    public partial class AgentsPage : Page, IPage
    {
        private IWindow _window;
        private readonly IAgentManager _agentManager;

        public AgentsPage(IWindow window, IAgentManager agentManager)
        {
            InitializeComponent();
            _window = window;
            _agentManager = agentManager;

            PopulateItems();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _window.SetHomePage();
        }

        public void PopulateItems()
        {
            ClearAllUi();
            List<object> allAgents = _agentManager.GetAllEntries();
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
                AbilitiesListBox.ItemsSource = _agentManager.GetAgentsAbilities(AgentNameListBox.SelectedItem);

                ClearTextBoxes();
                BioTextBox.Text = _agentManager.GetAgentDataStr(AgentNameListBox.SelectedItem, AgentManager.Fields.Bio);
                AgentClassLabel.Content = $"Agent Class: {_agentManager.GetAgentDataStr(AgentNameListBox.SelectedItem, AgentManager.Fields.Type)}";

                string imagePath = _agentManager.GetAgentDataStr(AgentNameListBox.SelectedItem, AgentManager.Fields.ImagePath);
                if (imagePath != null && imagePath != "")
                    AgentImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                else
                    AgentImage.Source = null;
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
                AbilityDiscriptionTextBox.Text = _agentManager.GetAbilityDiscription(AgentNameListBox.SelectedItem, AbilitiesListBox.SelectedItem);
            }
        }

        private void AddNewAgent(object sender, RoutedEventArgs e)
        {
            AddAgent addAgentWindow = new AddAgent(this, _agentManager);
            addAgentWindow.Show();
        }

        private void EditAgentButton_Click(object sender, RoutedEventArgs e)
        {
            if (AgentNameListBox.SelectedIndex >= 0)
            {
                EditAgent editAgent = new EditAgent(AgentNameListBox.SelectedItem, this, _agentManager);
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
                    _agentManager.RemoveEntry(AgentNameListBox.SelectedItem);
                    PopulateItems();
                }
            }
            else
            {
                MessageBox.Show("You need to select an agent to remove first");
            }
        }
    }
}
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ValorantDatabase;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AgentsPage.xaml
    /// </summary>
    public partial class AgentsPage : Page
    {
        MainPage _mainPage;
        MainWindow _window;
        AgentManager _agentManager;

        public AgentsPage(MainWindow window, MainPage page)
        {
            InitializeComponent();
            _mainPage = page;
            _window = window;
            _agentManager = new AgentManager();

            PopulateAgents();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _window.Content = _mainPage;
        }

        internal void PopulateAgents()
        {
            ClearAllUi();
            List<Agents> allAgents = _agentManager.GetAllAgents();
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

            BioTextBox.Text = "";
            AbilityDiscriptionTextBox.Text = "";
        }

        private void OnAgentSelected(object sender, SelectionChangedEventArgs e)
        {
            if (AgentNameListBox.SelectedIndex >= 0)
            {
                ClearAbilitiesListBox();
                AbilitiesListBox.ItemsSource = _agentManager.GetAgentsAbilities(AgentNameListBox.SelectedItem);

                BioTextBox.Text = _agentManager.GetAgentData(AgentNameListBox.SelectedItem, AgentManager.Fields.Bio);
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
                    _agentManager.RemoveAgent(AgentNameListBox.SelectedItem);
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
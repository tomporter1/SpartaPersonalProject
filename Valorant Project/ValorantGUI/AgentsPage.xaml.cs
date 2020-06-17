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
        AgentManager agentManager;
        AgentTypeManager typeManager;

        public AgentsPage(MainWindow window, MainPage page)
        {
            InitializeComponent();
            _mainPage = page;
            _window = window;
            agentManager = new AgentManager();
            typeManager = new AgentTypeManager();

            PopulateAgents();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _window.Content = _mainPage;
        }

        internal void PopulateAgents()
        {
            ClearAllUi();
            List<Agents> allAgents = agentManager.GetAllAgents();
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
                AbilitiesListBox.ItemsSource = agentManager.GetAgentsAbilities(AgentNameListBox.SelectedItem);

                BioTextBox.Text = agentManager.GetAgentData(AgentNameListBox.SelectedItem, AgentManager.Fields.Bio);
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
                AbilityDiscriptionTextBox.Text = agentManager.GetAbilityDiscription(AgentNameListBox.SelectedItem, AbilitiesListBox.SelectedItem);
            }
        }

        private void AddNewAgent(object sender, RoutedEventArgs e)
        {
            AddAgent addAgentWindow = new AddAgent(this);
            addAgentWindow.Show();
        }
    }
}

﻿using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using BussinessLayer.Managers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for EditAgent.xaml
    /// </summary>
    public partial class EditAgent : Window
    {
        private object _selectedAgent;
        private readonly IPage _agentsPage;
        private readonly IAgentManager _agentManager;

        public EditAgent(object selectedAgent, IPage agentsPage, IAgentManager agentManager)
        {
            InitializeComponent();
            _selectedAgent = selectedAgent;
            _agentsPage = agentsPage;
            _agentManager = agentManager;

            List<TextBox> textBoxes = new List<TextBox>()
            {
                NameTextBox,
                SigTextBox,
                SigDiscTextBox,
                UltTextBox,
                UltDiscTextBox,
                Normal1NameTextBox,
                Normal1DiscTextBox,
                Normal2NameTextBox,
                Normal2DiscTextBox,
                BioTextBox
            };

            for (int i = 0; i < textBoxes.Count; i++)
            {
                textBoxes[i].Text = _agentManager.GetAgentDataStr(_selectedAgent, (IAgentManager.Fields)i);
            }

            AgentTypeManager typeManager = new AgentTypeManager();
            AgentTypesComboBox.ItemsSource = typeManager.GetAllEntries();
            object agentType = _agentManager.GetAgentTypeObj(_selectedAgent);

            foreach (var item in AgentTypesComboBox.ItemsSource)
            {
                if (agentType.Equals(item))
                {
                    AgentTypesComboBox.SelectedItem = item;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Trim() != "" && AgentTypesComboBox.SelectedIndex >= 0)
            {
                AgentArgs args = new AgentArgs(
                    NameTextBox.Text.Trim(),
                    AgentTypesComboBox.SelectedItem,
                    SigTextBox.Text.Trim(),
                    SigDiscTextBox.Text.Trim(),
                    UltTextBox.Text.Trim(),
                    UltDiscTextBox.Text.Trim(),
                    Normal1NameTextBox.Text.Trim(),
                    Normal1DiscTextBox.Text.Trim(),
                    Normal2NameTextBox.Text.Trim(),
                    Normal2DiscTextBox.Text.Trim(),
                    BioTextBox.Text.Trim());

                _agentManager.UpdateEntry(_selectedAgent, args);
                _agentsPage.PopulateItems();
                this.Close();
            }
            else
            {
                if (NameTextBox.Text.Trim() == "")
                    nameLabel.Foreground = Brushes.Red;
                if (AgentTypesComboBox.SelectedIndex < 0)
                    TypesLabel.Foreground = Brushes.Red;

                MessageBox.Show("Please fill in the required fields");
            }
        }
    }
}
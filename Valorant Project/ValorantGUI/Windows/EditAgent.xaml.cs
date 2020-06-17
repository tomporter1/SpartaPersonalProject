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
    /// Interaction logic for EditAgent.xaml
    /// </summary>
    public partial class EditAgent : Window
    {
        object _selectedAgent;
        AgentsPage _agentsPage;
        public EditAgent(object selectedAgent, AgentsPage agentsPage)
        {
            InitializeComponent();
            _selectedAgent = selectedAgent;
            _agentsPage = agentsPage;

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

            AgentManager agentManager = new AgentManager();
            for (int i = 0; i < textBoxes.Count; i++)
            {
                textBoxes[i].Text = agentManager.GetAgentData(_selectedAgent, (AgentManager.Fields)i);
            }

            AgentTypeManager typeManager = new AgentTypeManager();
            AgentTypesListBox.ItemsSource = typeManager.GetAllTypes();
            object agentType = agentManager.GetAgentTypeObj(_selectedAgent);

            foreach (var item in AgentTypesListBox.ItemsSource)
            {
                if (agentType.Equals(item))
                {
                    AgentTypesListBox.SelectedItem = item;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AgentManagerArgs args = new AgentManagerArgs(
                NameTextBox.Text.Trim(),
                AgentTypesListBox.SelectedItem,
                SigTextBox.Text.Trim(),
                SigDiscTextBox.Text.Trim(),
                UltTextBox.Text.Trim(), 
                UltDiscTextBox.Text.Trim(), 
                Normal1NameTextBox.Text.Trim(), 
                Normal1DiscTextBox.Text.Trim(), 
                Normal2NameTextBox.Text.Trim(), 
                Normal2DiscTextBox.Text.Trim(), 
                BioTextBox.Text.Trim());

            new AgentManager().UpdateAgent(_selectedAgent, args);
            _agentsPage.PopulateAgents();
            this.Close();
        }
    }
}

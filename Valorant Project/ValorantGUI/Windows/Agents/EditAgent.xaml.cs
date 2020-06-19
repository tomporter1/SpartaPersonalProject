using BussinessLayer;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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

            for (int i = 0; i < textBoxes.Count; i++)
            {
                textBoxes[i].Text = _agentsPage.AgentManager.GetAgentData(_selectedAgent, (AgentManager.Fields)i);
            }

            AgentTypeManager typeManager = new AgentTypeManager();
            AgentTypesComboBox.ItemsSource = typeManager.GetAllEntries();
            object agentType = _agentsPage.AgentManager.GetAgentTypeObj(_selectedAgent);

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
            AgentManagerArgs args = new AgentManagerArgs(
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

            _agentsPage.AgentManager.UpdateEntry(_selectedAgent, args);
            _agentsPage.PopulateAgents();
            this.Close();
        }
    }
}

using BussinessLayer.Args;
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
        private AgentsPage _agentsPage;

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
                textBoxes[i].Text = _agentsPage.AgentManager.GetAgentDataStr(_selectedAgent, (AgentManager.Fields)i);
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

                _agentsPage.AgentManager.UpdateEntry(_selectedAgent, args);
                _agentsPage.PopulateAgents();
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
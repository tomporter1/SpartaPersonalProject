using BussinessLayer;
using System.Windows;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AddAgent.xaml
    /// </summary>
    public partial class AddAgent : Window
    {
        AgentsPage _agentsPage;

        public AddAgent(AgentsPage agentsPage)
        {
            InitializeComponent();

            PopulateTypes();
            _agentsPage = agentsPage;
        }

        internal void PopulateTypes()
        {
            AgentTypesListBox.ItemsSource = null;
            AgentTypesListBox.SelectedIndex = -1;
            AgentTypesListBox.ItemsSource = new AgentTypeManager().GetAllTypes();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (AgentTypesListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Class for this agent");
            }
            else
            {
                AgentManagerArgs args = new AgentManagerArgs
                    (
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
                        BioTextBox.Text.Trim()
                    );
                _agentsPage.AgentManager.AddNewAgent(args);
                _agentsPage.PopulateAgents();
                this.Close();
            }
        }

        private void AddTypeButton_Click(object sender, RoutedEventArgs e)
        {
            AddAgentType addAgentType = new AddAgentType(this);
            addAgentType.Show();
        }
    }
}

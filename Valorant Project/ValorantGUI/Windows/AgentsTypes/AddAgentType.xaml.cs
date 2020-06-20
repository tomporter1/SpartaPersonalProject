using BussinessLayer;
using System.Windows;
using System.Windows.Media;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AddAgentType.xaml
    /// </summary>
    public partial class AddAgentType : Window
    {
        private AddAgent _addAgent;
        private AgentClassesPage _agentClasses;

        public AddAgentType(AddAgent addAgent)
        {
            InitializeComponent();
            _addAgent = addAgent;
        }

        public AddAgentType(AgentClassesPage agentClasses)
        {
            InitializeComponent();
            _agentClasses = agentClasses;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Trim() != "")
            {
                new AgentTypeManager().AddNewEntry(new AgentTypeArgs(NameTextBox.Text.Trim()));

                if (_addAgent != null)
                {
                    _addAgent.PopulateTypes();
                }
                else if (_agentClasses != null)
                {
                    _agentClasses.PopulateTypes();
                }
                this.Close();
            }
            else
            {
                NameLabel.Foreground = Brushes.Red;

                MessageBox.Show("Please fill in the required fields");
            }
        }
    }
}

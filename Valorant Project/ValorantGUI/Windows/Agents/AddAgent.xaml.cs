using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using BussinessLayer.Managers;
using System.Windows;
using System.Windows.Media;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AddAgent.xaml
    /// </summary>
    public partial class AddAgent : Window
    {
        private IPage _agentsPage;
        private IBasicManager _agentManager;

        public AddAgent(IPage agentsPage, IBasicManager agentManager)
        {
            InitializeComponent();

            PopulateTypes();
            _agentsPage = agentsPage;
            _agentManager = agentManager;
        }

        internal void PopulateTypes()
        {
            AgentTypesComboBox.ItemsSource = null;
            AgentTypesComboBox.SelectedIndex = -1;
            AgentTypesComboBox.ItemsSource = new AgentTypeManager().GetAllEntries();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Trim() != "" && AgentTypesComboBox.SelectedIndex >= 0)
            {
                AgentArgs args = new AgentArgs
                    (
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
                        BioTextBox.Text.Trim()
                    );
                _agentManager.AddNewEntry(args);
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
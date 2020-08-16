using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using System.Windows;
using System.Windows.Media;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AddAgentType.xaml
    /// </summary>
    public partial class AddAgentType : Window
    {
        private readonly IPage _page;
        private readonly IBasicManager _typesManager;
        public AddAgentType(IPage agentPage, IBasicManager agentTypesManager)
        {
            InitializeComponent();
            _page = agentPage;
            _typesManager = agentTypesManager;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Trim() != "")
            {
                _typesManager.AddNewEntry(new AgentTypeArgs(NameTextBox.Text.Trim()));

                _page.PopulateItems();

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
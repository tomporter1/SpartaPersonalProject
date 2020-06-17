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
using ValorantDatabase;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AddAgentType.xaml
    /// </summary>
    public partial class AddAgentType : Window
    {
        AddAgent _addAgent;
        AgentClassesPage _agentClasses;

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
            new AgentTypeManager().AddNewAgentType(NameTextBox.Text.Trim());

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
    }
}

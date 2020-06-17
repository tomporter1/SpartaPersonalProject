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
        public AddAgentType(AddAgent addAgent)
        {
            InitializeComponent();
            _addAgent = addAgent;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            new AgentTypeManager().AddNewAgentType(NameTextBox.Text.Trim());
            _addAgent.PopulateTypes();
        }
    }
}

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
    /// Interaction logic for AddAgent.xaml
    /// </summary>
    public partial class AddAgent : Window
    {
        public AddAgent()
        {
            InitializeComponent();

            AgentTypesListBox.ItemsSource = new AgentTypeManager().GetAllTypes();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AgentManagerArgs args = new AgentManagerArgs
                (
                    NameTextBox.Text,
                    AgentTypesListBox.SelectedItem,
                    SigTextBox.Text,
                    SigDiscTextBox.Text,
                    UltTextBox.Text,
                    UltDiscTextBox.Text,
                    Normal1DiscTextBox.Text,
                    Normal1DiscTextBox.Text,
                    Normal2DiscTextBox.Text,
                    Normal2DiscTextBox.Text,
                    BioTextBox.Text
                );
            new AgentManager().AddNewAgent(args);            
        }
    }
}

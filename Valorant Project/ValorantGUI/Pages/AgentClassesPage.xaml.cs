using BussinessLayer;
using System.Windows;
using System.Windows.Controls;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AgentClassesPage.xaml
    /// </summary>
    public partial class AgentClassesPage : Page
    {
        MainWindow _window;
        internal AgentTypeManager AgentTypeManager { get; private set; }
        public AgentClassesPage(MainWindow window)
        {
            InitializeComponent();
            _window = window;
            AgentTypeManager = new AgentTypeManager();
            PopulateTypes();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TypesListBox.SelectedIndex >= 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure out want to delete {TypesListBox.SelectedItem}?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    AgentTypeManager.RemoveAgentType(TypesListBox.SelectedItem);
                    PopulateTypes();
                }
            }
            else
            {
                MessageBox.Show("Select a class to remove first.");
            }
        }

        internal void PopulateTypes()
        {
            TypesListBox.ItemsSource = null;
            TypesListBox.SelectedIndex = -1;
            NameTextBox.Text = "";
            TypesListBox.ItemsSource = AgentTypeManager.GetAllTypes();
            NameTextBox.IsEnabled = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddAgentType addAgentType = new AddAgentType(this);
            addAgentType.Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _window.SetHomePage();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypesListBox.SelectedIndex >= 0)
            {
                NameTextBox.Text = AgentTypeManager.GetTypeData(TypesListBox.SelectedItem, AgentTypeManager.Fields.Name);
                NameTextBox.IsEnabled = true;
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (TypesListBox.SelectedIndex >= 0)
            {
                AgentTypeManager.UpdateAgentType(TypesListBox.SelectedItem, NameTextBox.Text.Trim());
                PopulateTypes();
            }
            else
            {
                MessageBox.Show("Select a class to edit first.");
            }
        }
    }
}
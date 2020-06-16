﻿using BussinessLayer;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ValorantGUI
{
    /// <summary>
    /// Interaction logic for AgentsPage.xaml
    /// </summary>
    public partial class AgentsPage : Page
    {
        MainPage _mainPage;
        MainWindow _window;
        AgentManager agentManager;
        AgentTypeManager typeManager;

        public AgentsPage(MainWindow window, MainPage page)
        {
            InitializeComponent();
            _mainPage = page;
            _window = window;
            agentManager = new AgentManager();
            typeManager = new AgentTypeManager();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _window.Content= _mainPage;
        }

        private void PopulateAgents()
        {

        }
    }
}
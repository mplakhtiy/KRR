using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KRR.Controls
{
    /// <summary>
    /// Interaction logic for Entry.xaml
    /// </summary>
    public partial class Entry : UserControl
    {
        public Entry()
        {
            InitializeComponent();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            //add question if user want to delete
            String grid = null;
            //we assume that there is no the same name of fluent as agents etc
            foreach (Logic.Agent ag in MainWindow.agents)
            {
                if (AgentName.Content.ToString().Equals(ag.Name))
                {
                    MainWindow.agents.Remove(ag);
                   // ((MainWindow)System.Windows.Application.Current.MainWindow).AgentComboBox.Items.Remove(AgentName.Content.ToString());
                   grid = "AgentsGrid";
                    break;
                }
            }
            foreach (Logic.Action ac in MainWindow.actions)
            {
                if (AgentName.Content.ToString().Equals(ac.Name))
                {
                    MainWindow.actions.Remove(ac);
                    grid = "ActionsGrid";
                    break;
                }
            }
            foreach (Logic.Fluent fl in MainWindow.allFluents)
            {
                if (AgentName.Content.ToString().Equals(fl.Name))
                {
                    MainWindow.allFluents.Remove(fl);
                    grid = "FluentsGrid";
                    break;
                }
            }
            Update(grid);
        }
        public void Update(String grid)
        {
            int row1 = 0;
            switch (grid)
            {
                case "AgentsGrid":
                    ((MainWindow)System.Windows.Application.Current.MainWindow).AgentsGrid.RowDefinitions.Clear();
                    ((MainWindow)System.Windows.Application.Current.MainWindow).AgentsGrid.Children.Clear();
                    foreach (Logic.Agent ag in MainWindow.agents)
                    {
                        Entry entry = new Entry();
                        entry.AgentName.Content = ag.Name;
                        RowDefinition rowDefinition = new RowDefinition();
                        rowDefinition.Height = GridLength.Auto;
                        ((MainWindow)System.Windows.Application.Current.MainWindow).AgentsGrid.RowDefinitions.Add(rowDefinition);
                        Grid.SetRow(entry, row1);
                        ((MainWindow)System.Windows.Application.Current.MainWindow).AgentsGrid.Children.Add(entry);
                        row1++;
                    }
                    break;

                case "ActionsGrid":
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ActionsGrid.RowDefinitions.Clear();
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ActionsGrid.Children.Clear();
                    foreach (Logic.Action ag in MainWindow.actions)
                    {
                        Entry entry = new Entry();
                        entry.AgentName.Content = ag.Name;
                        RowDefinition rowDefinition = new RowDefinition();
                        rowDefinition.Height = GridLength.Auto;
                        ((MainWindow)System.Windows.Application.Current.MainWindow).ActionsGrid.RowDefinitions.Add(rowDefinition);
                        Grid.SetRow(entry, row1);
                        ((MainWindow)System.Windows.Application.Current.MainWindow).ActionsGrid.Children.Add(entry);
                        row1++;

                    }
                    break;

                case "FluentsGrid":
                    ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.RowDefinitions.Clear();
                    ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.Children.Clear();
                    foreach (Logic.Fluent ag in MainWindow.allFluents)
                    {
                        Entry entry = new Entry();
                        entry.AgentName.Content = ag.Name;
                        RowDefinition rowDefinition = new RowDefinition();
                        rowDefinition.Height = GridLength.Auto;
                        ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.RowDefinitions.Add(rowDefinition);
                        Grid.SetRow(entry, row1);
                        ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.Children.Add(entry);
                        row1++;
                    }
                    break;
            }
        }
    }
}

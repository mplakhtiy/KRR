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

namespace KRR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Logic.Agent> agents = new List<Logic.Agent>();
        public static List<Logic.Fluent> allFluents = new List<Logic.Fluent>(); // typed onces
        public static List<Logic.Action> actions = new List<Logic.Action>();
        public static List<Logic.Agent_Action> queries = new List<Logic.Agent_Action>();
        public static List<Logic.Fluent> initialliazed = new List<Logic.Fluent>(); //initiallywindow
        public static List<Logic.Fluent> temp = new List<Logic.Fluent>(); //realseswindow causesW
        public static Logic.Rule rules = new Logic.Rule();

        int row = 0;
        public static Controls.Add add2;

        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            Controls.Add add = new Controls.Add();
            add2 = new Controls.Add();
            Controls.Add add3 = new Controls.Add();
            Controls.Add2 add4 = new Controls.Add2();

            add.VerticalAlignment = VerticalAlignment.Bottom;
            add.AddButton.Name = "Agent";
            Grid.SetRow(add, 0);
            Grid.SetColumn(add, 0);
            MainGrid.Children.Add(add);

            add2.VerticalAlignment = VerticalAlignment.Bottom;
            add2.AddButton.Name = "Action";
            Grid.SetRow(add2, 1);
            Grid.SetColumn(add2, 0);
            MainGrid.Children.Add(add2);

            add4.VerticalAlignment = VerticalAlignment.Bottom;
            add4.AddButton.Name = "Fluent";
            Grid.SetRow(add4, 2);
            Grid.SetColumn(add4, 0);
            MainGrid.Children.Add(add4);

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Windows.RuleWindow ruleWindow = new Windows.RuleWindow();
            ruleWindow.ShowDialog();
        }

        public static void AddAgent(String agentName)
        {
            Logic.Agent agent = new Logic.Agent(agentName);
            agents.Add(agent);
        }

        public static void AddAction(String actionName)
        {
            Logic.Action action = new Logic.Action(actionName);
            actions.Add(action);
        }

        public static void AddFluent(String fluentName, bool isTrue)
        {
            Logic.Fluent fluent = new Logic.Fluent(fluentName, isTrue);
            allFluents.Add(fluent);
        }

        private void AddQuery_Click(object sender, RoutedEventArgs e)
        {
            //.IsEnabled = false;

            Controls.RuleControls.Query temp;
            if (row > 0)
            {
                temp = QueryGrid.Children[row - 1] as Controls.RuleControls.Query;
                temp.ActionComboBox.IsEnabled = false;
                temp.AgentComboBox.IsEnabled = false;

                Logic.Agent_Action agAc;

                foreach (Logic.Agent a in agents)
                {
                    foreach (Logic.Action ac in actions)
                    {
                        if (temp.AgentComboBox.Name.Equals(a.Name) && temp.ActionComboBox.Name.Equals(ac.Name))
                        {
                            agAc = new Logic.Agent_Action(a, ac);
                            queries.Add(agAc);
                        }
                    }

            }
            }



            ColumnDefinition rowDefinition = new ColumnDefinition();
            rowDefinition.Width = GridLength.Auto;
            QueryGrid.ColumnDefinitions.Add(rowDefinition);
            
            Controls.RuleControls.Query query = new Controls.RuleControls.Query();
            Grid.SetColumn(query, row);
            QueryGrid.Children.Add(query);
            row++;
            
        }

        private void Perform_Click(object sender, RoutedEventArgs e)
        {
            Logic.Main.TheMostImportantMethod(rules, initialliazed, allFluents, queries);
        }

        private void ClearQuery_Click(object sender, RoutedEventArgs e)
        {
            QueryGrid.Children.Clear();
            QueryGrid.ColumnDefinitions.Clear();
            queries.Clear();
            row = 0;

            ColumnDefinition rowDefinition = new ColumnDefinition();
            rowDefinition.Width = GridLength.Auto;
            QueryGrid.ColumnDefinitions.Add(rowDefinition);

            Controls.RuleControls.Query query = new Controls.RuleControls.Query();
            Grid.SetColumn(query, row);
            QueryGrid.Children.Add(query);
            row++;
        }
    }
}

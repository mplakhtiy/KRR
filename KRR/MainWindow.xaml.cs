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
            //add2.Name.Name = "Action";
            Grid.SetRow(add2, 1);
            Grid.SetColumn(add2, 0);
            MainGrid.Children.Add(add2);


            /*add3.VerticalAlignment = VerticalAlignment.Bottom;
            add3.AddButton.Name = "Statement";
            Grid.SetRow(add3,0);
            Grid.SetColumn(add3, 1);
            MainGrid.Children.Add(add3);*/

            add4.VerticalAlignment = VerticalAlignment.Bottom;
            add4.AddButton.Name = "Fluent";
            Grid.SetRow(add4, 2);
            Grid.SetColumn(add4, 0);
            MainGrid.Children.Add(add4);

            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //new window
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
            ColumnDefinition rowDefinition = new ColumnDefinition();
            rowDefinition.Width = GridLength.Auto;
            QueryGrid.ColumnDefinitions.Add(rowDefinition);

            
            Controls.RuleControls.Query query = new Controls.RuleControls.Query();
            Grid.SetColumn(query, row);
            QueryGrid.Children.Add(query);
            row++;
            
            
        }

        /*private void AgentsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActionsGrid.Children.Clear();
            ActionsGrid.RowDefinitions.Clear();
            foreach (var ag in MainWindow.agents)
            {
                if (ag.Name.Equals(AgentsComboBox.SelectedValue.ToString()))
                {
                    RowDefinition rowDefinition = new RowDefinition();
                    rowDefinition.Height = GridLength.Auto;
                    ActionsGrid.RowDefinitions.Add(rowDefinition);

                   // if (add2.Name.Name.Equals(null))
                   // {
                        Controls.ActionControl actioncontrol = new Controls.ActionControl(ag);
                        ActionsGrid.Children.Add(actioncontrol);
                  //  }
                }
            }
        }
        */
    }
}

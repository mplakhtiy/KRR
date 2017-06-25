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
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : UserControl
    {
        public Add()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Check() == false)
                return;
            Button b = sender as Button;
            string name = b.Name.ToString();
            //sorry for text, there is no simpler workaround
            int row=0, col=0;
            if (name.Equals("Agent"))
            {
                row = 0;
                col = 0;
            }
            else if (name.Equals("Action"))
            {

                row = 1;
                col = 0;
            }
            else if (name.Equals("Statement"))
            {
                row = 0;
                col = 1;
            }
            
            else if (name.Equals("Fluent"))
            {
                row = 2;
                col = 0;
            }

            Entry entry = new Entry();
            entry.AgentName.Content = Name.Text.ToString();
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;
            
            switch (row)
            {
                case 0:

                    if (col == 0) //agents
                    {
                        ((MainWindow)System.Windows.Application.Current.MainWindow).AgentsGrid.RowDefinitions.Add(rowDefinition);
                        int row1 = ((MainWindow)System.Windows.Application.Current.MainWindow).AgentsGrid.RowDefinitions.Count;
                        Grid.SetRow(entry, row1 - 1);
                        ((MainWindow)System.Windows.Application.Current.MainWindow).AgentsGrid.Children.Add(entry);

                       
                        MainWindow.AddAgent(entry.AgentName.Content.ToString());
                        // ((MainWindow)System.Windows.Application.Current.MainWindow).AgentsComboBox.Items.Add(entry.AgentName.Content.ToString());
                        //((MainWindow)System.Windows.Application.Current.MainWindow).AgentComboBox.Items.Add(entry.AgentName.Content.ToString());
                    }
                    
                    else
                    {
                       
                    }
                    break;
            
                case 1:
                    //actions

                    ((MainWindow)System.Windows.Application.Current.MainWindow).ActionsGrid.RowDefinitions.Add(rowDefinition);
                    int row2 = ((MainWindow)System.Windows.Application.Current.MainWindow).ActionsGrid.RowDefinitions.Count;
                    Grid.SetRow(entry, row2 - 1);
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ActionsGrid.Children.Add(entry);


                    MainWindow.AddAction(entry.AgentName.Content.ToString());
                    //((MainWindow)System.Windows.Application.Current.MainWindow).AgentsComboBox.Items.Add(entry.AgentName.Content.ToString());


                    /*
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ActionsGrid.Children.Clear();
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ActionsGrid.RowDefinitions.Clear();
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ActionsGrid.RowDefinitions.Add(rowDefinition);
                    Logic.Action ac = new Logic.Action(entry.AgentName.Content.ToString());
                    //to the agent
                    Logic.Agent temp = new Logic.Agent(null);
                    foreach (var ag in MainWindow.agents)
                    {
                        if (ag.Name.Equals(((MainWindow)System.Windows.Application.Current.MainWindow).AgentsComboBox.SelectedValue))
                        {
                            ag.Actions.Add(ac);
                            temp = ag;
                        }
                    }
                    Controls.ActionControl actioncontrol = new ActionControl(temp);
                    

                    //here wrong
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ActionsGrid.Children.Add(actioncontrol);
                    */
                    break;

                case 2: //fleunt
                    ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.RowDefinitions.Add(rowDefinition);
                    int ble4 = ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.RowDefinitions.Count;
                    Grid.SetRow(entry, ble4 - 1);
                    ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.Children.Add(entry);

                    MainWindow.AddFluent(entry.AgentName.Content.ToString(),false); //as default false
                    break;
                   
            }

            Name.Text = null;
           
        }
        public bool Check()
        {
            
            return (!String.IsNullOrEmpty(Name.Text.ToString()));
        }

    }
}

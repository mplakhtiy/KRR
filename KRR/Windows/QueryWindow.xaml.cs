using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace KRR.Windows
{
    /// <summary>
    /// Interaction logic for FluentsWindow.xaml
    /// </summary>
    public partial class QueryWindow : Window
    {
        static public String query = String.Empty;
        public static String statement = String.Empty;



        public QueryWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            statement = String.Empty;
            InitializeComponent();
            foreach (var ag in MainWindow.agents)
            {
                AgentComboBox.Items.Add(ag.Name.ToString());
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string from = String.Empty;
            if (MainWindow.initiallyEvaluator != null)
                from = " from " +MainWindow.initiallyEvaluator.Original;
            ((MainWindow)System.Windows.Application.Current.MainWindow).Goal.Text = query +  from;
            query = String.Empty;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddGoal_Click(object sender, RoutedEventArgs e)
        {
            Controls.FormulaWindow window = new Controls.FormulaWindow("goal");
            window.ShowDialog();

            queryTextBlock.Text += statement;
        }



        private void AgentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var a in MainWindow.agents)
            {
                if (AgentComboBox.SelectedItem.ToString().Equals(a.Name.ToString()))
                    MainWindow.agentPerform = a;
            }
            query += " ";
            query += MainWindow.agentPerform.Name.ToString();

            queryTextBlock.Text = query;

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //uunable all
            PossiblyComboBox.IsEnabled = true;
            QueryTypeComboBox.IsEnabled = true;
            Agent.Visibility = Visibility.Hidden;
            AgentComboBox.Visibility = Visibility.Hidden;
            AddGoalButton.Visibility = Visibility.Hidden;
            PossiblyComboBox.SelectedIndex = -1;
            QueryTypeComboBox.SelectedIndex = -1;
        }

        private void PossiblyComboBox_DataContextChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox a = sender as ComboBox;
            if (a.SelectedIndex >= 0)
            {
                ComboBoxItem item = a.SelectedItem as ComboBoxItem;
                query = item.Content.ToString();
                query += " ";///

                MainWindow.choosenPossibly = a.SelectedIndex;
                PossiblyComboBox.IsEnabled = false;
                queryTextBlock.Text = query;
            }

        }

        private void QueryTypeComboBox_DataContextChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox a = sender as ComboBox;
            if (a.SelectedIndex >= 0)
            {
                ComboBoxItem item = a.SelectedItem as ComboBoxItem;
                query += item.Content.ToString(); ///
                QueryTypeComboBox.IsEnabled = false;
                queryTextBlock.Text = query;

                MainWindow.choosenType = a.SelectedIndex;

                //index
                switch (item.Name.ToString())
                {
                    case "goal":
                        AddGoalButton.Visibility = Visibility.Visible;
                        break;
                    case "agent":
                        Agent.Visibility = Visibility.Visible;
                        AgentComboBox.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }
        }


     
         

    }
}

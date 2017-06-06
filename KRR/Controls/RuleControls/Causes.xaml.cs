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

namespace KRR.Controls.RuleControls
{
    /// <summary>
    /// Interaction logic for Causes.xaml
    /// </summary>
    public partial class Causes : UserControl
    {
        public Causes()
        {
            InitializeComponent();
            foreach (var a in MainWindow.agents)
            {
                AgentComboBox.Items.Add(a.Name.ToString());
            }
            foreach (var a in MainWindow.actions)
            {
                ActionComboBox.Items.Add(a.Name.ToString());
                
            }
        }

        private void AddFluent_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Windows.RuleWindow.btnClicked = btn.Name.ToString();
            Windows.InitiallyWindow fluent = new Windows.InitiallyWindow("causes");
            fluent.ShowDialog();
        }

        private void AddFluent2_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Windows.RuleWindow.btnClicked = btn.Name.ToString();
            Windows.FluentsWindow fluent = new Windows.FluentsWindow();
            fluent.ShowDialog();
        }

        private void AgentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var a in MainWindow.agents)
                if (a.Name.Equals(AgentComboBox.SelectedItem.ToString()))
                {
                    Windows.RuleWindow.ag = a;
                }
        }

        private void ActionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var a in MainWindow.actions)
                if (a.Name.Equals(ActionComboBox.SelectedItem.ToString()))
                {
                    Windows.RuleWindow.ac = a;
                }
        }
    }
}

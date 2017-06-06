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
    public partial class Releases : UserControl
    {
        public Releases()
        {
            InitializeComponent();
            foreach (var fl in MainWindow.agents)
            {
                AgentComboBox.Items.Add(fl.Name.ToString());
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
            Windows.InitiallyWindow fluent = new Windows.InitiallyWindow();
            fluent.ShowDialog();
        }

        private void AddFluent2_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Windows.RuleWindow.btnClicked = btn.Name.ToString();
            Windows.FluentsWindow fluent = new Windows.FluentsWindow();
            fluent.ShowDialog();
        }

        //private void AgentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ActionComboBox.Items.Clear();
        //    ComboBox combobox = sender as ComboBox;
        //    foreach (var fl in MainWindow.agents)
        //    {
        //        if (fl.Name.ToString().Equals(combobox.SelectedItem.ToString()))
        //        {
        //            foreach (var ac in fl.Actions)
        //                ActionComboBox.Items.Add(ac.Name.ToString());
        //        }
        //    }
        //}
    }
}

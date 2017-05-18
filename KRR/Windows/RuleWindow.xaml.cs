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
using System.Windows.Shapes;

namespace KRR.Windows
{
    /// <summary>
    /// Interaction logic for RuleWindow.xaml
    /// </summary>
    public partial class RuleWindow : Window
    {
        public RuleWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int row = 0;
            int col = 1;

            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;

            Controls.Entry entry = new Controls.Entry();

            // HERE WE WRITE THE CONCATENATION OF THE RULE AS TEXT
            //entry.AgentName.Content

            ((MainWindow)System.Windows.Application.Current.MainWindow).StatementsGrid.RowDefinitions.Add(rowDefinition);
            int ble2 = ((MainWindow)System.Windows.Application.Current.MainWindow).StatementsGrid.RowDefinitions.Count;
            Grid.SetRow(entry, ble2 - 1);
            ((MainWindow)System.Windows.Application.Current.MainWindow).StatementsGrid.Children.Add(entry);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox i = sender as ComboBox;
            switch (i.SelectedIndex)
             {
                case 0:
                    if(Rule.Children.Count!=0)
                        Rule.Children.Clear();
                    Controls.RuleControls.Causes causes = new Controls.RuleControls.Causes();
                    Rule.Children.Add(causes);
                    break;
                case 1:
                    if (Rule.Children.Count != 0)
                        Rule.Children.Clear();
                    Controls.RuleControls.Releases Releases = new Controls.RuleControls.Releases();
                    Rule.Children.Add(Releases);
                    break;
                case 2:
                    if (Rule.Children.Count != 0)
                        Rule.Children.Clear();
                    Controls.RuleControls.Always Always = new Controls.RuleControls.Always();
                    Rule.Children.Add(Always);
                    break;
                case 3:
                    if (Rule.Children.Count != 0)
                        Rule.Children.Clear();
                    Controls.RuleControls.Impossible Impossible = new Controls.RuleControls.Impossible();
                    Rule.Children.Add(Impossible);
                    break;
            }
             
        }
    }
}

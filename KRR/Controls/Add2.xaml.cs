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
    public partial class Add2 : UserControl
    {
        public Add2()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Check() == false)
                return;
            Button b = sender as Button;

            Entry entry = new Entry();
            entry.AgentName.Content = Name.Text.ToString();
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;

            ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.RowDefinitions.Add(rowDefinition);
            int ble = ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.RowDefinitions.Count;
            Grid.SetRow(entry, ble - 1);
            ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.Children.Add(entry);

            Logic.Fluent fluent = new Logic.Fluent(Name.Text.ToString(), false);
            MainWindow.allFluents.Add(fluent);



            Name.Text = null;

        }
        public bool Check()
        {

            return (!String.IsNullOrEmpty(Name.Text.ToString()));
        }

    }
}

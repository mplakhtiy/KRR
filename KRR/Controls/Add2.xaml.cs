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

            Entry2 entry2 = new Entry2();
            entry2.FluentName.Content = Name.Text.ToString();
            entry2.Boolean.Content = null;
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;

            ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.RowDefinitions.Add(rowDefinition);
            int ble = ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.RowDefinitions.Count;
            Grid.SetRow(entry2, ble - 1);
            ((MainWindow)System.Windows.Application.Current.MainWindow).FluentsGrid.Children.Add(entry2);

            Logic.Fluent fluent = new Logic.Fluent(Name.Text.ToString(), false);
            MainWindow.fleunts.Add(fluent);



            Name.Text = null;

        }
        public bool Check()
        {

            return (!String.IsNullOrEmpty(Name.Text.ToString()));
        }

    }
}

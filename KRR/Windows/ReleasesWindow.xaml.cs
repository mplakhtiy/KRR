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
    public partial class ReleasesWindow : Window
    {
        int col = 0;
        static public String fluents = String.Empty;
        public Logic.Fluent fluent = null;
        private String name = null;

        public ReleasesWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();
           
            foreach (var fl in MainWindow.allFluents)
            {
                FluentComboBox.Items.Add(fl.Name.ToString());
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {

            
            MainWindow.temp.Add(fluent);
               

            MainWindow.statement3 = fluents;

            fluents = String.Empty;
            fluentsTextBlock.Children.Clear();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddFluent1_Click(object sender, RoutedEventArgs e)
        {
            if (FluentComboBox.SelectedIndex >= 0)
            {
                if (fluent != null)
                    return;
                RowDefinition colDefinition = new RowDefinition();
                colDefinition.Height = GridLength.Auto;
                fluentsTextBlock.RowDefinitions.Add(colDefinition);

                TextBlock text = new TextBlock();
                text.Text = FluentComboBox.Text.ToString();
                text.FontSize = 15;
                text.FontFamily = new FontFamily("Impact");
                text.HorizontalAlignment = HorizontalAlignment.Center;
                text.VerticalAlignment = VerticalAlignment.Center;
                text.TextWrapping = TextWrapping.Wrap;



                Grid.SetRow(text, col);
                col++;
                fluentsTextBlock.Children.Add(text);
                fluents += text.Text;

                fluent = new Logic.Fluent(FluentComboBox.SelectedItem.ToString(), false); //false because must pass sth


                FluentComboBox.Items.Remove(FluentComboBox.SelectedItem);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            fluents = String.Empty;
            fluentsTextBlock.Children.Clear();
            fluent = null;
            FluentComboBox.Items.Clear();

            foreach (var fl in MainWindow.allFluents)
            {
                FluentComboBox.Items.Add(fl.Name.ToString());
            }
        }
    }
}

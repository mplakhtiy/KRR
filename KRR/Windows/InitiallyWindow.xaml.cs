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
    public partial class InitiallyWindow : Window
    {
        int col = 0;
        static public String fluents = String.Empty;
        public List<Logic.Fluent> tempfluent = new List<Logic.Fluent>();

        public InitiallyWindow()
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
            foreach (Logic.Fluent fl in tempfluent)
            {
                MainWindow.initialliazed.Add(fl);
            }
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

            RowDefinition colDefinition = new RowDefinition();
            colDefinition.Height = GridLength.Auto;
            fluentsTextBlock.RowDefinitions.Add(colDefinition);

            TextBlock text = new TextBlock();
            text.Text = FluentComboBox.Text.ToString() + " " + isTrue.IsChecked.ToString();
            text.FontSize = 15;
            text.FontFamily = new FontFamily("Impact");
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.TextWrapping = TextWrapping.Wrap;

            FluentComboBox.Items.Remove(FluentComboBox.SelectedItem);

            Grid.SetRow(text, col);
            col++;
            fluentsTextBlock.Children.Add(text);
            fluents += text.Text;

            bool check;
            if (isTrue.IsChecked == null)
                check = false;
            else
                check = true;
            Logic.Fluent fluent = new Logic.Fluent(FluentComboBox.Text.ToString(), check);
            tempfluent.Add(fluent);

        }

        

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            fluents = String.Empty;
            fluentsTextBlock.Children.Clear();
            tempfluent.Clear();
        }
    }
}

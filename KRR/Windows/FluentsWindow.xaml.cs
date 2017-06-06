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
    public partial class FluentsWindow : Window
    {
        int col = 0;
        bool notClicked = false;
        static public String fluents = String.Empty;
        public List<Logic.Fluent> temp = new List<Logic.Fluent>();
        public static List<List<Logic.Fluent>> _if = new List<List<Logic.Fluent>>();

        public FluentsWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();
            foreach (var fl in MainWindow.allFluents)
            {
                FluentComboBox.Items.Add(fl.Name.ToString());
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            _if.Add(temp);
            MainWindow.statement2 = fluents;


            fluents = String.Empty;
            fluentsTextBlock.Children.Clear();
            notClicked = false;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Not_Click(object sender, RoutedEventArgs e)
        {
            Not.IsEnabled = false;
            Or.IsEnabled = false;
            And.IsEnabled = false;

            notClicked = true;

            ColumnDefinition colDefinition = new ColumnDefinition();
            colDefinition.Width = GridLength.Auto;
            fluentsTextBlock.ColumnDefinitions.Add(colDefinition);

            TextBlock text = new TextBlock();
            text.Text = " NOT";
            text.FontSize = 16;
            text.FontFamily = new FontFamily("Impact");
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, col);
            col++;
            fluentsTextBlock.Children.Add(text);
            fluents += text.Text;
        }

        private void And_Click(object sender, RoutedEventArgs e)
        {

            And.IsEnabled = false;
            Or.IsEnabled = false;
            Not.IsEnabled = true;

            ColumnDefinition colDefinition = new ColumnDefinition();
            colDefinition.Width = GridLength.Auto;
            fluentsTextBlock.ColumnDefinitions.Add(colDefinition);

            TextBlock text = new TextBlock();
            text.Text = " AND";
            text.FontSize = 16;
            text.FontFamily = new FontFamily("Impact");
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, col);
            col++;
            fluentsTextBlock.Children.Add(text);
            fluents += text.Text;
        }

        private void Or_Click(object sender, RoutedEventArgs e)
        {
            And.IsEnabled = false;
            Or.IsEnabled = false;
            Not.IsEnabled = true;

            _if.Add(temp);
            temp.Clear();

            ColumnDefinition colDefinition = new ColumnDefinition();
            colDefinition.Width = GridLength.Auto;
            fluentsTextBlock.ColumnDefinitions.Add(colDefinition);

            TextBlock text = new TextBlock();
            text.Text = " OR";
            text.FontSize = 16;
            text.FontFamily = new FontFamily("Impact");
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, col);
            col++;
            fluentsTextBlock.Children.Add(text);
            fluents += text.Text;
        }

        private void AddFluent1_Click(object sender, RoutedEventArgs e)
        {
            And.IsEnabled = true;
            Or.IsEnabled = true;
            Not.IsEnabled = false;

            ColumnDefinition colDefinition = new ColumnDefinition();
            colDefinition.Width = GridLength.Auto;
            fluentsTextBlock.ColumnDefinitions.Add(colDefinition);

            TextBlock text = new TextBlock();
            text.Text = " " + FluentComboBox.Text.ToString();
            text.FontSize = 16;
            text.FontFamily = new FontFamily("Impact");
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, col);
            col++;
            fluentsTextBlock.Children.Add(text);
            fluents += text.Text;

            Logic.Fluent fluent = new Logic.Fluent(FluentComboBox.Text.ToString(), notClicked);
            temp.Add(fluent);

            notClicked = false;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            And.IsEnabled = true;
            Or.IsEnabled = true;
            Not.IsEnabled = true;
            FluentComboBox.IsEnabled = true;
            AddFluent1Button.IsEnabled = true;
            fluents = String.Empty;
            fluentsTextBlock.Children.Clear();
        }
    }
}

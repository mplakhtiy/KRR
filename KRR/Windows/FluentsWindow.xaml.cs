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
        static public String fluents = String.Empty;

        public FluentsWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();
            foreach (var fl in MainWindow.allFluents)
            {
                FluentComboBox.Items.Add(fl.Name.ToString());
                FluentComboBox2.Items.Add(fl.Name.ToString());
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            //add here new fluents as one to list of fluents or somehow add this for checking later if condition is true or not
            //add to list
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(RuleWindow))
                {
                    Grid maingrid = window.Content as Grid;
                    Grid grid = maingrid.Children[3] as Grid;
                    UserControl control = grid.Children[0] as UserControl;
                    Grid g = control.Content as Grid;
                    Label label =null;
                    if (Windows.RuleWindow.btnClicked.Equals("AddFluent"))
                        label = g.Children[7] as Label; //fluent1
                    else
                        label = g.Children[11] as Label;
                    label.Content = fluents.ToString();
                }
            }

            fluents = String.Empty;
            fluentsTextBlock.Children.Clear();
            this.Close();


        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Not_Click(object sender, RoutedEventArgs e)
        {
            Not.IsEnabled = false;

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
            FluentComboBox.IsEnabled = false;
            AddFluent1Button.IsEnabled = false;
            FluentComboBox2.IsEnabled = true;
            AddFluent2Button.IsEnabled = true;

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
            FluentComboBox2.IsEnabled = true;
            AddFluent2Button.IsEnabled = true;

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
            FluentComboBox.IsEnabled = false;
            AddFluent1Button.IsEnabled = false;
            FluentComboBox2.IsEnabled = false;
            AddFluent2Button.IsEnabled = false;

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

        }

        private void AddFluent2_Click(object sender, RoutedEventArgs e)
        {
            And.IsEnabled = false;
            Or.IsEnabled = false;
            Not.IsEnabled = false;
            FluentComboBox.IsEnabled = false;
            AddFluent1Button.IsEnabled = false;
            FluentComboBox2.IsEnabled = false;
            AddFluent2Button.IsEnabled = false;

            ColumnDefinition colDefinition = new ColumnDefinition();
            colDefinition.Width = GridLength.Auto;
            fluentsTextBlock.ColumnDefinitions.Add(colDefinition);
            TextBlock text = new TextBlock();
            text.Text = " " + FluentComboBox2.Text.ToString();
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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            And.IsEnabled = true;
            Or.IsEnabled = true;
            Not.IsEnabled = true;
            FluentComboBox.IsEnabled = true;
            AddFluent1Button.IsEnabled = true;
            FluentComboBox2.IsEnabled = true;
            AddFluent2Button.IsEnabled = true;
            fluents = String.Empty;
            fluentsTextBlock.Children.Clear();
        }
    }
}

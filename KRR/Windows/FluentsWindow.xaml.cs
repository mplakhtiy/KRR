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
    /// Interaction logic for FluentsWindow.xaml
    /// </summary>
    public partial class FluentsWindow : Window
    {
        int col=0;
        static public String fluents = null;

        public FluentsWindow()
        {
            InitializeComponent();
            foreach (var fl in MainWindow.fleunts)
            {
                FluentComboBox.Items.Add(fl.Name.ToString());
                FluentComboBox2.Items.Add(fl.Name.ToString());
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
         
            this.Close();
            
            
        }
        
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Not_Click(object sender, RoutedEventArgs e)
        {
            ColumnDefinition colDefinition = new ColumnDefinition();
            colDefinition.Width = GridLength.Auto;
            fluentsTextBlock.ColumnDefinitions.Add(colDefinition);
            
            TextBlock text = new TextBlock();
            text.Text = " NOT ";
            text.FontSize = 12;
            text.FontFamily = new FontFamily( "Impact");
            Grid.SetColumn(text, col);
            col++;
            fluentsTextBlock.Children.Add(text);
            fluents += text.Text;
            }

        private void And_Click(object sender, RoutedEventArgs e)
        {
            ColumnDefinition colDefinition = new ColumnDefinition();
            colDefinition.Width = GridLength.Auto;
            fluentsTextBlock.ColumnDefinitions.Add(colDefinition);

            TextBlock text = new TextBlock();
            text.Text = " AND ";
            text.FontSize = 12;
            text.FontFamily = new FontFamily("Impact");
            Grid.SetColumn(text, col);
            col++;
            fluentsTextBlock.Children.Add(text);
            fluents += text.Text;
        }

        private void Or_Click(object sender, RoutedEventArgs e)
        {
            ColumnDefinition colDefinition = new ColumnDefinition();
            colDefinition.Width = GridLength.Auto;
            fluentsTextBlock.ColumnDefinitions.Add(colDefinition);

            TextBlock text = new TextBlock();
            text.Text = " OR ";
            text.FontSize = 12;
            text.FontFamily = new FontFamily("Impact");
            Grid.SetColumn(text, col);
            col++;
            fluentsTextBlock.Children.Add(text);
            fluents += text.Text;
        }

        private void AddFluent1_Click(object sender, RoutedEventArgs e)
        {
            And.IsEnabled = true;
            Or.IsEnabled = true;
            FluentComboBox2.IsEnabled = true;
            AddFluent2Button.IsEnabled = true;

            ColumnDefinition colDefinition = new ColumnDefinition();
            colDefinition.Width = GridLength.Auto;
            fluentsTextBlock.ColumnDefinitions.Add(colDefinition);

            TextBlock text = new TextBlock();
            text.Text = FluentComboBox.Text.ToString();
            text.FontSize = 12;
            text.FontFamily = new FontFamily("Impact");
            Grid.SetColumn(text, col);
            col++;
            fluentsTextBlock.Children.Add(text);
            fluents += text.Text;

        }

        private void AddFluent2_Click(object sender, RoutedEventArgs e)
        {
            ColumnDefinition colDefinition = new ColumnDefinition();
            colDefinition.Width = GridLength.Auto;
            fluentsTextBlock.ColumnDefinitions.Add(colDefinition);
            TextBlock text = new TextBlock();
            text.Text = " " + FluentComboBox2.Text.ToString() + " ";
            text.FontSize = 12;
            text.FontFamily = new FontFamily("Impact");
            Grid.SetColumn(text, col);
            col++;
            fluentsTextBlock.Children.Add(text);
        }

        private void FluentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

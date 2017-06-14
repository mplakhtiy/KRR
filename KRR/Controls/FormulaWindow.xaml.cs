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
using KRR.Logic.TruthTable;
using System.Text.RegularExpressions;


namespace KRR.Controls
{
    /// <summary>
    /// Interaction logic for FormulaWindow.xaml
    /// </summary>
    public partial class FormulaWindow : Window
    {
        public string checkIfOrCauses;
        public Evaluator evaluator;
        public FormulaWindow(string check)
        {
            checkIfOrCauses = check;
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Format the input text to change the operators
                Query.Text = FormatInput(Query.Text.Trim());

                String original = Query.Text;

                Dictionary<string, char> dict = ConvertFluentsToChar(Query.Text);
                String convertedText = ReplaceFluentsWithChar(dict, Query.Text);

                //Create an instance of the evaluator class
                evaluator = new Evaluator(convertedText, original);

                //Update the truth Table, tree view and the plan textbox
                TruthTable.ItemsSource = evaluator.EvaluateQuery(dict);


            }
            catch
            {
                //If, at all anything goes wrong
                //The only possible case is when the symbols are unbalanced
                //Or, there is no input in the text-box
                if (Query.Text.Length == 0) { MessageBox.Show("No Query in the Text Box", "No Query"); }
                else { MessageBox.Show("Warning: Unbalanced Symbols found in the Stack", "Error in Query"); }
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
           
            switch (checkIfOrCauses)
            {
                case "causes":
                    MainWindow.evaluator = this.evaluator;
                    MainWindow.statement3 = Query.Text;
                    break;
                case "if_list":
                    MainWindow.ifListEval = this.evaluator;
                    break;
                case "always":
                    MainWindow.AlwaysHeader = this.evaluator.Original;
                    MainWindow.alwaysEvaluator = this.evaluator;
                    
                    break;
                default:
                    break;
            }
            

            this.Close();
        }

        public String ReplaceFluentsWithChar(Dictionary<string, char> dict, string query)
        {

            //foreach (KeyValuePair<string, char> pair in dict)
            //{
            //    query = query.Replace(pair.Key.ToString(), pair.Value.ToString());
            //}

            //return query;


            string result = query;
            foreach (KeyValuePair<string, char> pair in dict)
            {
                string replace = pair.Value.ToString();
                result = Regex.Replace(result, string.Format(@"\b{0}\b", pair.Key.ToString()), replace);
            }

            return result;


        }
   
        public Dictionary<string, char> ConvertFluentsToChar(string inputText)
        {

            Dictionary<string, char> dict = new Dictionary<string, char>();

            List<char> lstChar = new List<char>()
                        //{'a','b','c','d','e','f', 'g','h', 'i','j','k','l','m','n','o'};
            { 'α', 'β', 'γ', 'δ', 'ε', 'ζ', 'η', 'θ', 'ι', 'κ', 'λ', 'μ', 'ν', 'ξ', 'ο', 'π', 'ρ', 'σ', 'τ', 'υ', 'φ', 'χ', 'ψ', 'ω'};


            string fluent = "";

            for (int i = 0; i < inputText.Length; i++)
            {
                if (Char.IsLetter(inputText[i]) == true && i != inputText.Length - 1)
                {
                    fluent += inputText[i];
                }
                else if (fluent != "" && (Evaluator.prec.Contains(inputText[i]) == true || inputText[i] == ' '))
                {
                    if (!dict.ContainsKey(fluent))
                    {
                        dict.Add(fluent, lstChar[0]);
                        fluent = "";
                        lstChar.RemoveAt(0);
                    }
                    else
                    {
                        fluent = "";
                    }

                }

                else if (Char.IsLetter(inputText[i]) == true && i == inputText.Length - 1)
                {
                    fluent += inputText[i];
                    if (!dict.ContainsKey(fluent))
                    {
                        dict.Add(fluent, lstChar[0]);
                    }
                }

                else if (i == inputText.Length - 1)
                {
                    if (!dict.ContainsKey(fluent))
                    {
                        fluent += inputText[i];
                        dict.Add(fluent, lstChar[0]);
                    }
                }
            }

            return dict;

        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Correct the parameters of the TabContainer
            TabContainer.Width = e.NewSize.Width - 40;
            TabContainer.Height = e.NewSize.Height - 150;

            //Correct the parameters of the Go Button
            var goMargin = Go.Margin;
            goMargin.Left = TabContainer.Width + TabContainer.Margin.Left - Go.Width;
            Go.Margin = goMargin;

            //Correct the parameters of the Query TextBox
            var queryMargin = Query.Margin;
            queryMargin.Left = TabContainer.Margin.Left;
            Query.Width = goMargin.Left - 10 - queryMargin.Left;
            Query.Margin = queryMargin;
        }
        /// <summary>
        /// Formats the input to replace the dummy operators with their actual symbol
        /// </summary>
        /// <param name="inputText">The text which contains the boolean expression</param>
        /// <returns>The formatted string with the correct operator syymbols</returns>
        private string FormatInput(string inputText)
        {
            //Replacing all the dummy operators with their actual symbols
            inputText = inputText.Replace('~', '¬');
            inputText = inputText.Replace('|', '∨');
            inputText = inputText.Replace('&', '∧');
            inputText = inputText.Replace('-', '↔');
            inputText = inputText.Replace('>', '→');

            for (int i = 0; i < inputText.Length; i++)
            {
                if (Char.IsLetter(inputText[i]) == true || Evaluator.prec.Contains(inputText[i]) == true || inputText[i] == ' ') { }
                else { inputText = inputText.Remove(i); }
            }
            return inputText;
        }

        private void Query_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox sendText = sender as TextBox;
            sendText.Text = FormatInput(sendText.Text);
            sendText.Select(sendText.Text.Length, 0);
        }

        private void Query_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                //switch ((sender as TextBox).Name)
                //{
                //    case "Query":
                //        Button_Click(sender, new EventArgs());
                //        break;

                //}
            }
        }
    }
}

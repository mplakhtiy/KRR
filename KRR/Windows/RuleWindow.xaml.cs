using KRR.Logic;
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
using KRR.Controls;

namespace KRR.Windows
{
    /// <summary>
    /// Interaction logic for RuleWindow.xaml
    /// </summary>
    public partial class RuleWindow : Window
    {
        public static String btnClicked = String.Empty;
        public static Logic.Agent ag = new Logic.Agent(null);
        public static Logic.Action ac = new Logic.Action(null);

        public RuleWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            int row = 0;
            int col = 1;
            bool ifClicked = false;

            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;

            List<List<Logic.Fluent>> orList = new List<List<Logic.Fluent>>();

            if (MainWindow.ifListEval!= null)
            {
                bool[] formulaResult = MainWindow.ifListEval.GetResultData();
                bool neverBool = true;
                for (int i = 0; i < formulaResult.Length; i++)
                {
                    if (formulaResult[i])
                    {
                        neverBool = false;
                    }
                }
                if (neverBool)
                {
                    orList.Add(new List<Fluent> { new Fluent("-", false) });

                }
                else {

                    for (int i = 0; i < formulaResult.Length; i++)
                    {
                        if (formulaResult[i])
                        {

                            List<Fluent> andList = new List<Fluent>();
                            foreach (Fluent fluent in MainWindow.allFluents)
                            {
                                if (MainWindow.ifListEval.EvalPlan.ContainsKey(fluent.Name))
                                {
                                    andList.Add(new Fluent(fluent.Name, MainWindow.ifListEval.EvalPlan[fluent.Name].fieldResult[i]));
                                }
                            }

                            orList.Add(andList);
                        }
                    }
                }
            }

            switch (ruleComboBox.SelectedIndex)
            {
                case 0: //causes
                    if (ag != null && ac != null)
                    {
                        Logic.Agent_Action agAc = new Logic.Agent_Action(ag, ac);
                        List<Logic.Fluent> tempp = MainWindow.temp.ToList();
                        List<List<Logic.Fluent>> _iff = orList.ToList();
                        if (_iff.Count > 0)
                            ifClicked = true;
                        Logic.Rules.CausesIf rule = new Logic.Rules.CausesIf(MainWindow.evaluator, agAc, tempp, _iff);

                        MainWindow.statement = rule.ToString();
                        MainWindow.rules.AddRule(rule);
                    }
                    break;
                case 1: //realalssesesese
                    if (ag != null && ac != null)
                    {
                        Logic.Agent_Action agAc2 = new Logic.Agent_Action(ag, ac);
                        List<Logic.Fluent> tempp = MainWindow.temp.ToList();
                        List<List<Logic.Fluent>> _iff = orList.ToList();
                        if (_iff.Count > 0)
                            ifClicked = true;
                        Logic.Rules.ReleasesIf rule2 = new Logic.Rules.ReleasesIf(agAc2, tempp, _iff);

                        MainWindow.statement = rule2.ToString();
                        MainWindow.rules.AddRule(rule2);
                    }
                    break;
                case 3: //initilly
                    break;
                case 2:
                    if(MainWindow.AlwaysEvaluator!=null)
                    MainWindow.statement = "always " + MainWindow.AlwaysEvaluator.EvalPlan.Last().Key;
                    break;
                

            }

            MainWindow.ifListEval = null;
            MainWindow.temp.Clear();
            FluentsWindow._if.Clear();
            ag = null;
            ac = null;

            Controls.EntryStatement entry = new Controls.EntryStatement();
            if (ifClicked)
                entry.AgentName.Content = MainWindow.statement + MainWindow.statement3 + " if " + MainWindow.statement2;
            else
                entry.AgentName.Content = MainWindow.statement + MainWindow.statement3;
            ((MainWindow)System.Windows.Application.Current.MainWindow).StatementsGrid.RowDefinitions.Add(rowDefinition);
            int ble2 = ((MainWindow)System.Windows.Application.Current.MainWindow).StatementsGrid.RowDefinitions.Count;
            Grid.SetRow(entry, ble2 - 1);
            ((MainWindow)System.Windows.Application.Current.MainWindow).StatementsGrid.Children.Add(entry);


            MainWindow.statement = null;
            MainWindow.statement2 = null;
            MainWindow.statement3 = null;
            this.Close();
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
                    if (Rule.Children.Count != 0)
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
                    Controls.FormulaWindow Always = new Controls.FormulaWindow("always");
                    Always.Show();
                    break;
                case 3:
                    if (Rule.Children.Count != 0)
                        Rule.Children.Clear();
                    Controls.RuleControls.Initially Initially = new Controls.RuleControls.Initially();
                    Rule.Children.Add(Initially);
                    break;
                
            }
        }
    }
}

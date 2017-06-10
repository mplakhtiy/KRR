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
    /// Interaction logic for Entry.xaml
    /// </summary>
    public partial class EntryStatement : UserControl
    {
        public EntryStatement()
        {
            InitializeComponent();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            String grid = null;
            //we assume that there is no the same name of fluent as agents etc
            foreach (var rule in Logic.Rule.causesIfRules)
            {
                //this should be change its shity
                if (AgentName.Content.ToString().Contains(rule.agent_action.ToString()) &&
                    AgentName.Content.ToString().Contains("causes") 
                    //AgentName.Content.ToString().Contains(rule.change[0].Name.ToString()) &&
                   // AgentName.Content.ToString().Contains(rule.change[0].IsTrue.ToString())
                    // AgentName.Content.ToString().Contains(rule._if.) &&
                    )
                {
                    Logic.Rule.causesIfRules.Remove(rule);

                    break;
                }
            }

            Update();
        }
        public void Update()
        {
            AgentName.Content = "removed";
        }
    }
}

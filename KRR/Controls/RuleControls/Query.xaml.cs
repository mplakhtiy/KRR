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

namespace KRR.Controls.RuleControls
{
    /// <summary>
    /// Interaction logic for Causes.xaml
    /// </summary>
    public partial class Query : UserControl
    {
        public Query()
        {
            InitializeComponent();
            foreach (var a in MainWindow.agents)
            {
                AgentComboBox.Items.Add(a.Name.ToString());
            }
            foreach (var a in MainWindow.actions)
            {
                ActionComboBox.Items.Add(a.Name.ToString());
            }
        }
    }
}

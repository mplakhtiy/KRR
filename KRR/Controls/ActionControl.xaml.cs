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
    /// Interaction logic for ActionControl.xaml
    /// </summary>
    public partial class ActionControl : UserControl
    {
        public ActionControl(Logic.Agent ag)
        {
            InitializeComponent();
            foreach (var action in ag.Actions)
            {
                Entry entry = new Entry();
                entry.AgentName.Content = action.Name;
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                ActionsGrid.RowDefinitions.Add(rowDefinition);

                ActionsGrid.Children.Add(entry);


            }
        }
    }
}

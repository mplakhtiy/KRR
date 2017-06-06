﻿using System;
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
    public partial class Always : UserControl
    {
        public Always()
        {
            InitializeComponent();
            foreach (var fl in MainWindow.allFluents)
            {
                FluentComboBox.Items.Add(fl.Name.ToString());
                FluentComboBox2.Items.Add(fl.Name.ToString());
            }
        }

    }
}

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
    public partial class Causes : UserControl
    {
        public Causes()
        {
            InitializeComponent();
        }

        private void AddFluent_Click(object sender, RoutedEventArgs e)
        {
            Windows.FluentsWindow fluent = new Windows.FluentsWindow();
            fluent.ShowDialog();
            //new window
        }

        private void AddFluent2_Click(object sender, RoutedEventArgs e)
        {
            Windows.FluentsWindow fluent = new Windows.FluentsWindow();
            fluent.ShowDialog();
        }


        private void change()
        {
            fluent1.Content = Windows.FluentsWindow.fluents;
        }
    }
}

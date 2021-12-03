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
using System.Windows.Shapes;

namespace MathGraph.DialogBoxes
{
    /// <summary>
    /// Логика взаимодействия для RemoveEdge.xaml
    /// </summary>
    public partial class RemoveEdge : Window
    {
        public RemoveEdge()
        {
            InitializeComponent();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        public string NameStart
        {
            get => txb_VertexNameStart.Text;
        }
        public string NameEnd
        {
            get => txb_VertexNameEnd.Text;
        }
        
    }
}

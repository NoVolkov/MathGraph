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
    /// Логика взаимодействия для RemoveVertex.xaml
    /// </summary>
    public partial class RemoveVertex : Window
    {
        public RemoveVertex()
        {
            InitializeComponent();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        public string Name
        {
            get => txb_VertexName.Text;
        }
    }
}

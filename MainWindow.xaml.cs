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
using MathGraph.Models;

namespace MathGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Models.MathGraph graph;
        public MainWindow()
        {
            InitializeComponent();
            graph = new Models.MathGraph("Новый граф");
            Title = graph.GetNameGraph();
        }

        private void btn_ModeGraph(object sender, RoutedEventArgs e)
        {

        }
        private void btn_AddVertex(object sender, RoutedEventArgs e)
        {

        }private void btn_RemoveVertex(object sender, RoutedEventArgs e)
        {

        }private void btn_AddEdge(object sender, RoutedEventArgs e)
        {

        }private void btn_RemoveEdge(object sender, RoutedEventArgs e)
        {

        }private void btn_ChangeWeight(object sender, RoutedEventArgs e)
        {

        }
        
    }
}

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
using MathGraph.DialogBoxes;
//using MathGraph.DisplayedModels;
using MathGraph.Models;

namespace MathGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Models.MathGraph graph;
        int countVerties;
        public MainWindow()
        {
            InitializeComponent();
            graph = new Models.MathGraph("Новый граф");
            Title = graph.GetNameGraph();
            countVerties = 0;
        }

        private void btn_ModeGraph(object sender, RoutedEventArgs e)
        {
            if (graph.mode == MODEGRAPH.DIR)
            {
                graph.SetMode(MODEGRAPH.UNDIR);
                txt_ModeGraph.Text = "Неориентированный";
            }
            else
            {
                txt_ModeGraph.Text = "Ориентированный";
                graph.SetMode(MODEGRAPH.DIR);
            }
            UpdateListVertices();
            UpdateListEdges();
        }
        private void btn_AddVertex(object sender, RoutedEventArgs e)
        {
            AddVertex nv = new AddVertex();
            string name = "";
            if (nv.ShowDialog() == true)
            {
                name = nv.Name;
                Vertex v = graph.AddVertex(name);
                if (name.Equals(""))
                {
                    MessageBox.Show("Пустое название.");
                    btn_AddVertex(sender, e);
                    return;
                }
                if (v is null)
                {
                    MessageBox.Show("Такая вершина уже существует.");
                    btn_AddVertex(sender, e);
                    return;
                }
                v.Number = countVerties;
                countVerties++;
                TabControlVertices.Items.Add(v);
            }
        }
        private void UpdateListVertices()
        {
            TabControlVertices.Items.Clear();
            countVerties = 0;
            foreach (Vertex v in graph.GetVertices())
            {
                v.Number = countVerties;
                countVerties++;
                TabControlVertices.Items.Add(v);
            }
        }
        private void UpdateListEdges()
        {
            TabControlEdges.Items.Clear();
            foreach (Edge e in graph.GetEdges())
            {
                TabControlEdges.Items.Add(e);
            }

        }
        private void btn_RemoveVertex(object sender, RoutedEventArgs e)
        {
            RemoveVertex rv = new RemoveVertex();
            string name = "";
            if (rv.ShowDialog() == true)
            {
                name = rv.Name;
                if (name.Equals(""))
                {
                    MessageBox.Show("Пустое название.");
                    btn_RemoveVertex(sender, e);
                    return;
                }
                if (!graph.RemoveVertex(name))
                {
                    MessageBox.Show("Такой вершины не существует.");
                    btn_RemoveVertex(sender, e);
                    return;
                }
            }
            UpdateListVertices();
            UpdateListEdges();

        }
        private void btn_AddEdge(object sender, RoutedEventArgs e)
        {
            AddEdge nv = new AddEdge();

            string nameStart = "";
            string nameEnd = "";
            decimal edgeWeight ;
            
            if (nv.ShowDialog() == true)
            {
                nameStart = nv.NameStart;
                nameEnd = nv.NameEnd;
                if (nv.EdgeWeight.Equals(""))
                {
                    edgeWeight = 1;
                }
                else
                {
                    edgeWeight = decimal.Parse(nv.EdgeWeight);
                }

                if (nameStart.Equals(""))
                {
                    MessageBox.Show("Пустое название начальной вершины.");
                    btn_AddEdge(sender, e);
                    return;
                }
                if (nameEnd.Equals(""))
                {
                    MessageBox.Show("Пустое название конечной вершины.");
                    btn_AddEdge(sender, e);
                    return;
                }
                
                Vertex vStart = graph.GetVertices().Find(x=>x.GetName().Equals(nameStart));
                Vertex vEnd = graph.GetVertices().Find(x=>x.GetName().Equals(nameEnd));
                if (vStart is null)
                {
                    MessageBox.Show("Такой начальной вершины не существует.");
                    btn_AddEdge(sender, e);
                    return;
                }
                if (vEnd is null)
                {
                    MessageBox.Show("Такой конечной вершины не существует..");
                    btn_AddEdge(sender, e);
                    return;
                }
                if (vStart.Equals(vEnd))
                {
                    edgeWeight = 1;
                }
                Edge edge=graph.AddEdge(vStart, vEnd, edgeWeight);
                TabControlEdges.Items.Add(edge);
                UpdateListVertices();
            }
        }
        private void btn_RemoveEdge(object sender, RoutedEventArgs e)
        {

        }
        private void btn_ChangeWeight(object sender, RoutedEventArgs e)
        {

        }
        private void btn_MoveVertex(object sender, RoutedEventArgs e)
        {

        }
        private void btn_Dijkstra(object sender, RoutedEventArgs e)
        {

        }

        
    }
}

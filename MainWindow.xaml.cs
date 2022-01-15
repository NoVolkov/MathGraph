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
            //TabControlVertices.AddHandler(GridView.Che,new RoutedEventHandler(editColorVertex_Click));
        }
        private void editColorVertex_Selected(object sender, RoutedEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item is not null && item.IsSelected)
            {
                int i = TabControlVertices.SelectedIndex;
                Vertex v = graph.GetVertices().Find(x => x.Number.Equals(i));
                v.color = new SolidColorBrush(Colors.Black);
                updateCanvas();
                MessageBox.Show(v.color.ToString());
            }
                
            /*int i = TabControlVertices.SelectedIndex;
            Vertex v=graph.GetVertices().Find(x => x.Number.Equals(i));
            v.color= new SolidColorBrush(Colors.Black);
            updateCanvas();
            MessageBox.Show(v.color.ToString());*/
        }
        private void createVertexView(Vertex v)
        {
            Ellipse e = new Ellipse() { Width = 20, Height = 20 };
            e.Stroke = new SolidColorBrush(Colors.Red);
            e.Fill = v.color;
            
            e.StrokeThickness = 2;
            double left = v.point.X - e.Width/2;
            double top = v.point.Y - e.Height/2;
            e.Margin = new Thickness(left, top, 0, 0);
            FieldPaint.Children.Add(e);
        }
        private void updateCanvas()
        {
            FieldPaint.Children.Clear();
            foreach(Vertex v in graph.GetVertices().ToArray())
            {
                createVertexView(v);
            }

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
                createVertexView(v);
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
                    //++преобразовывать вводимую точку в запятую
                    edgeWeight = decimal.Parse(nv.EdgeWeight.Replace('.',','));
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
            RemoveEdge re = new RemoveEdge();
            string nameStart = "";
            string nameEnd = "";
            decimal edgeWeight;

            if (re.ShowDialog() == true)
            {
                nameStart = re.NameStart;
                nameEnd = re.NameEnd;
                if (re.EdgeWeight.Equals(""))
                {
                    edgeWeight = 1;
                }
                else
                {
                    edgeWeight = decimal.Parse(re.EdgeWeight);
                }

                if (nameStart.Equals(""))
                {
                    MessageBox.Show("Пустое название начальной вершины.");
                    btn_RemoveEdge(sender, e);
                    return;
                }
                if (nameEnd.Equals(""))
                {
                    MessageBox.Show("Пустое название конечной вершины.");
                    btn_RemoveEdge(sender, e);
                    return;
                }

                Vertex vStart = graph.GetVertices().Find(x => x.GetName().Equals(nameStart));
                Vertex vEnd = graph.GetVertices().Find(x => x.GetName().Equals(nameEnd));
                if (vStart is null)
                {
                    MessageBox.Show("Такой начальной вершины не существует.");
                    btn_RemoveEdge(sender, e);
                    return;
                }
                if (vEnd is null)
                {
                    MessageBox.Show("Такой конечной вершины не существует..");
                    btn_RemoveEdge(sender, e);
                    return;
                }
                
                if(!graph.RemoveEdge(vStart,vEnd,edgeWeight))
                {
                    MessageBox.Show("Такого ребра не существует.");
                    btn_RemoveEdge(sender, e);
                    return;
                }
            }
            UpdateListVertices();
            UpdateListEdges();
        }
        private void btn_ChangeWeight(object sender, RoutedEventArgs e)
        {
            ChangeWeightEdge re = new ChangeWeightEdge();
            string nameStart = "";
            string nameEnd = "";
            decimal edgeOldWeight;
            decimal edgeNewWeight;

            if (re.ShowDialog() == true)
            {
                nameStart = re.NameStart;
                nameEnd = re.NameEnd;
                if (re.EdgeOldWeight.Equals(""))
                {
                    edgeOldWeight = 1;
                }
                else
                {
                    edgeOldWeight = decimal.Parse(re.EdgeOldWeight);
                }

                if (nameStart.Equals(""))
                {
                    MessageBox.Show("Пустое название начальной вершины.");
                    btn_ChangeWeight(sender, e);
                    return;
                }
                if (nameEnd.Equals(""))
                {
                    MessageBox.Show("Пустое название конечной вершины.");
                    btn_ChangeWeight(sender, e);
                    return;
                }
                
                Vertex vStart = graph.GetVertices().Find(x => x.GetName().Equals(nameStart));
                Vertex vEnd = graph.GetVertices().Find(x => x.GetName().Equals(nameEnd));
                if (vStart is null)
                {
                    MessageBox.Show("Такой начальной вершины не существует.");
                    btn_ChangeWeight(sender, e);
                    return;
                }
                if (vEnd is null)
                {
                    MessageBox.Show("Такой конечной вершины не существует..");
                    btn_ChangeWeight(sender, e);
                    return;
                }
                if (re.EdgeNewWeight.Equals(""))
                {
                    MessageBox.Show("Пустое значение веса.");
                    btn_ChangeWeight(sender, e);
                    return;
                }
                edgeNewWeight = decimal.Parse(re.EdgeNewWeight);

                if (!graph.UpdateWeightEdge(vStart, vEnd, edgeOldWeight,edgeNewWeight))
                {
                    MessageBox.Show("Такого ребра не существует.");
                    btn_ChangeWeight(sender, e);
                    return;
                }
            }
            UpdateListVertices();
            UpdateListEdges();
        }
        private void btn_MoveVertex(object sender, RoutedEventArgs e)
        {

        }
        private void btn_Dijkstra(object sender, RoutedEventArgs e)
        {
            VerticesDijkstra re = new VerticesDijkstra();
            string nameStart = "";
            string nameEnd = "";
            if (re.ShowDialog() == true)
            {
                nameStart = re.NameStart;
                nameEnd = re.NameEnd;
                
                if (nameStart.Equals(""))
                {
                    MessageBox.Show("Пустое название начальной вершины.");
                    btn_Dijkstra(sender, e);
                    return;
                }
                if (nameEnd.Equals(""))
                {
                    MessageBox.Show("Пустое название конечной вершины.");
                    btn_Dijkstra(sender, e);
                    return;
                }

                Vertex vStart = graph.GetVertices().Find(x => x.GetName().Equals(nameStart));
                Vertex vEnd = graph.GetVertices().Find(x => x.GetName().Equals(nameEnd));
                if (vStart is null)
                {
                    MessageBox.Show("Такой начальной вершины не существует.");
                    btn_Dijkstra(sender, e);
                    return;
                }
                if (vEnd is null)
                {
                    MessageBox.Show("Такой конечной вершины не существует..");
                    btn_Dijkstra(sender, e);
                    return;
                }
                graph.MethodDijkstra(vStart,vEnd);
                MessageBox.Show("Метод Дейкстры");
            }


            /*int i = TabControlVertices.SelectedIndex;
            MessageBox.Show(graph.GetVertices()[i].Name);*/
        }

        #region Кнопки меню
        private void btn_SaveFile(object sender, RoutedEventArgs e)
        {
            SaveToFromFile.SaveGraph(graph);
        }
        private void btn_SaveFileAs(object sender, RoutedEventArgs e)
        {
            SaveToFromFile.SaveGraphAs(graph);
        }
        private void btn_OpenFile(object sender, RoutedEventArgs e)
        {
            graph=SaveToFromFile.OpenMathGraph();
            Title = graph.GetNameGraph();
            countVerties = 0;
            UpdateListVertices();
            UpdateListEdges();
        }
        #endregion
    }
}

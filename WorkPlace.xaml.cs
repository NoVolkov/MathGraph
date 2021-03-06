using MathGraph.Models;
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

namespace MathGraph
{
    /// <summary>
    /// Логика взаимодействия для WorkPlace.xaml
    /// </summary>
    public partial class WorkPlace : Page
    {
        List<Vertex> verticesViews;
        List<Edge> edgesViews;
        public WorkPlace()
        {
            InitializeComponent();
        }
        public Canvas GetCanvas()
        {
            return FieldPaint;
        }
        public void AddVertices(List<Vertex> vertices)
        {
            this.verticesViews = vertices;
        }
        public void AddEdges(List<Edge> edges)
        {
            this.edgesViews = edges;
        }
        public void UpdatePage()
        {
            FieldPaint.Children.Clear();

            foreach (Vertex v in verticesViews)
            {
                Ellipse e = new Ellipse() { Width = 10, Height = 10 };
                double left = v.point.X - 5;
                double top = v.point.Y - 5;
                e.Margin = new Thickness(left, top, 0, 0);
                FieldPaint.Children.Add(e);
            }

        }
    }
}

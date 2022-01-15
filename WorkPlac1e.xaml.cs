using YiYan127.WPF.Arrows;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MathGraph.Models;

namespace MathGraph
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        List<Vertex> verticesViews;
        List<Edge> edgesViews;
        public Page1()
        {
            InitializeComponent();
            /*Arrow a = new Arrow() { X1 = 100, Y1 = 0, X2 = 600, Y2 = 600, StrokeThickness=10, Stroke = new SolidColorBrush(Colors.Red), HeadHeight = 15, HeadWidth = 15 };
            this.Content = a;
            this.AllowDrop = true;*/
            //Rectangle r = new Rectangle() { Height = 2000, Width = 2000, Fill = new SolidColorBrush(Colors.White) };

            /*ArrowLineWithText a = new ArrowLineWithText() { StartPoint = new Point(0, 0), EndPoint = new Point(600, 600), Fill = new SolidColorBrush(Colors.Red), Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 10, Text = "123456789", TextAlignment = TextAlignment.Center, IsTextUp = true, ArrowEnds=ArrowEnds.End };
            FieldPaint.Children.Add(a); */

            //FieldPaint.Children.Remove(a);
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
            
            foreach(Vertex v in verticesViews)
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

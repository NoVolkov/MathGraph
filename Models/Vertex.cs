using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace MathGraph.Models
{
    public class Vertex
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public Point point { get; set; }
        public int Quantity { get; set; }
        private List<Edge> adjacentEdges;
        public Brush color;
        public decimal A;
        public Vertex(string name)
        {
            this.Name = name;
            adjacentEdges = new List<Edge>();
            Quantity = 0;
            point = new Point(100, 100);
            color = new SolidColorBrush(Colors.Aquamarine);
        }
        public void SetName(string name)
        {
            this.Name = name;
        }
        public string GetName()
        {
            return Name;
        }
        public void AddAdjEdge(Edge edge)
        {
            adjacentEdges.Add(edge);
            Quantity++;
        }
        public void RemoveAdjEdge(Edge edge)
        {
            adjacentEdges.Remove(edge);
            Quantity--;
        }
        public List<Edge> GetAdjEdges()=>adjacentEdges;
    }
}

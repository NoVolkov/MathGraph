using System.Collections.Generic;
using System.Windows;

namespace MathGraph.Models
{
    public class Vertex
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public Point point { get; set; }
        public int Quantity { get; set; }
        private List<Edge> adjacentEdges;
        public Vertex(string name)
        {
            this.Name = name;
            adjacentEdges = new List<Edge>();
            Quantity = 0;
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
        public List<Edge> GetAdjEdges()=>adjacentEdges;
    }
}

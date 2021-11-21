using System.Collections.Generic;

namespace MathGraph.Models
{
    public class Vertex
    {
        private string Name { get; set; }
        private List<Edge> adjacentEdges;
        public Vertex(string name)
        {
            this.Name = name;
            adjacentEdges = new List<Edge>();
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
        }
        public List<Edge> GetAdjEdges()=>adjacentEdges;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MathGraph.Models
{
    public class Edge
    {
        private Vertex StartV { get; set; }
        private Vertex EndV { get; set; }
        public string StartVertex 
        {
            get { return StartV.GetName(); }
        }
        public string EndVertex {
            get { return EndV.GetName(); } 
        }
        public decimal Weight { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        public Edge(Vertex startVertex, Vertex endVertex, decimal weight) {
            this.StartV = startVertex;
            this.EndV = endVertex;
            this.Weight = weight;

        }

        public void SetWeight(decimal weight)
        {
            this.Weight = weight;
        }
        public decimal getWeight()
        {
            return Weight;
        }
        public Vertex getStartVertex() =>StartV;
        public Vertex getEndVertex() => EndV;




    }
}

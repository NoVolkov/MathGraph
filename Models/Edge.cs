using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Models
{
    public class Edge
    {
        private Vertex StartV { get; set; }
        private Vertex EndV { get; set; }
        private decimal Weight { get; set; }
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

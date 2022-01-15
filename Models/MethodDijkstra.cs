using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGraph.Models
{
    public class MethodDijkstra
    {
        private Vertex S;
        private Vertex E;
        private Dictionary<Vertex,decimal>[] D;
        private Vertex[] T;
        public MethodDijkstra(List<Vertex> vertices, Vertex start, Vertex end)
        {
            D = new Dictionary<Vertex, decimal>[vertices.Count];
            T = new Vertex[vertices.Count];
            S = start;
            E = end;
        }
    }
}

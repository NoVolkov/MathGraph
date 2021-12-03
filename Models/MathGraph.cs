using System;
using System.Collections.Generic;

namespace MathGraph.Models
{
    public class MathGraph
    {
        private string NameGraph { get; set; }
        //Сделать так чтобы ребро было либо одно такое, либо использовать уникальные идентификаторы для их различия.
        private List<Vertex> vertices;
        private List<Edge> edges;
        public MODEGRAPH mode;
        public MathGraph(string nameGraph)
        {
            NameGraph = nameGraph;
            vertices = new List<Vertex>();
            edges = new List<Edge>();
            mode = MODEGRAPH.UNDIR;
        }

        public string GetNameGraph() => NameGraph;
        public void SetNameGraph(string name) 
        {
            NameGraph = name;
        }
        //  Добавление вершины с индивидуальным именем
        //  Возвращает созданный объект, если вершина усппешно добавлена,
        //  null - если вершина с таким именем существует.
        public Vertex AddVertex(string name)
        {
            Vertex v = null;
            if (vertices.Count == 0 ||
                vertices.Exists((x) => x.GetName().Equals(name)) == false
                )
            {
                v = new Vertex(name);
                vertices.Add(v);
            }
            return v;

        }
        public Vertex AddVertex(Vertex vertex)
        {
            return AddVertex(vertex.GetName());
        }

        /// Удаляет вершину и смежные рёбра по имени этой вершины.
        /// </summary>
        /// <param name="name"> Имя удаляемой вершины.</param>
        public bool RemoveVertex(string name)
        {
            Vertex v = vertices.Find((x) => x.GetName().Equals(name));
            if (v is null) return false;
            foreach (Edge e in v.GetAdjEdges())
            {
                edges.Remove(e);
            }
            vertices.Remove(v);
            return true;
        }
        /// <summary>
        /// Добавляет ребро для двух вершин.
        /// </summary>
        /// <param name="startVertex">Начальная вершина.</param>
        /// <param name="endVertex">Конечная вершина.</param>
        /// <param name="weight">Вес ребра. По умолчанию равен 1.</param>
        public Edge AddEdge(Vertex startVertex, Vertex endVertex, decimal weight=1) {
            Edge e = new Edge(startVertex, endVertex, weight);
            edges.Add(e);
            startVertex.AddAdjEdge(e);
            if (mode == MODEGRAPH.UNDIR) endVertex.AddAdjEdge(e);
            return e;
        }
        
        public void RemoveEdge(Vertex startVertex, Vertex endVertex,decimal weight)
        {
            Edge e = edges.Find(x => x.getStartVertex().Equals(startVertex) && x.getEndVertex().Equals(endVertex) && x.getWeight().Equals(weight));
            edges.Remove(e);
        }
        /// <summary>
        /// Изменение режима графа из неориентированного в ориентированный и обратно.
        /// </summary>
        /// <param name="mode">Enum MODEGRAPH для режима графа: DIR, UNDIR.</param>
        public void SetMode(MODEGRAPH mode)
        {
            if (mode == MODEGRAPH.DIR)
            {
                //Добавление новых рёбер с другим напрвлением отличным от имеющихся.
                this.mode = mode;
                List<Edge> newEdges = new List<Edge>();
                foreach (Edge e in edges)
                {
                    newEdges.Add(new Edge(e.getEndVertex(), e.getStartVertex(), e.getWeight()));
                }
                edges.AddRange(newEdges);
            }
            else
            {
                if (mode == MODEGRAPH.UNDIR)
                {
                    //Удаление рёбер обратных данному
                    this.mode = mode;
                    //List<Edge> newEdges = new List<Edge>();
                    foreach (Vertex v in vertices)
                    {
                        foreach(Edge e in v.GetAdjEdges())
                        {
                            Edge eForRemove =edges.Find(x => 
                                e.getStartVertex().Equals(x.getEndVertex()) &&
                                e.getEndVertex().Equals(x.getStartVertex())
                                );
                            edges.Remove(eForRemove);//ошибка null из-за отсутствия ребра????
                        }
                    }
                }

            }
        }
        public void MethodDijkstra()
        {

        }
        public List<Vertex> GetVertices() => vertices;
        public List<Edge> GetEdges() => edges;
    }
}

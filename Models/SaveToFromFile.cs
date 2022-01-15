using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MathGraph.Models
{
    public class SaveToFromFile
    {
        public static void SaveGraph(MathGraph mg)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Список рёбер (*.loe)|*.loe|Матрица смежности (*.amx)|*.amx";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Title = "Сохранить граф";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName;
                if (GetExtensionFile(filename)=="loe")
                {
                    SaveToListOfEdges(filename, mg);
                }
                else
                    if (GetExtensionFile(filename) == "amx")
                    {
                        SaveToAdjMatrix(filename, mg);
                    }
                
            }
        }
        public static void SaveGraphAs(MathGraph mg)
        {

        }
        /*public static MathGraph OpenGraph()
        {
            return null;
        }*/
        
        private static void SaveToListOfEdges(string fileName, MathGraph mg)
        {
            string res = "";
            List<Vertex> vertices = mg.GetVertices();
            res += vertices.Count + "\n";
            if (vertices.Count != 0)
            {
                res += "(";
                foreach (Vertex v in mg.GetVertices()) res += v.Name + "\t";
                res += ")\n";
                foreach (Edge e in mg.GetEdges().ToArray())
                {
                    res += e.StartVertex + " " + e.EndVertex + " " + e.getWeight() + "\n";
                }
            }
            
            File.WriteAllTextAsync(fileName, res);
        }
        private static void SaveToAdjMatrix(string fileName, MathGraph mg)
        {
            string res = "";
            //int c = 0;
            res += mg.GetVertices().Count + "\n";
            List<Vertex> vertices = mg.GetVertices();
            for(int i = 0; i < vertices.Count; i++)
            {
                for(int j = 0; j < vertices.Count; j++)
                {
                    if (i == j || vertices[i].GetAdjEdges().Find(x => x.getEndVertex() == vertices[j]) == null)
                    {
                        res += "0\t";
                    }
                    else 
                    {
                        res += mg.GetEdges().Find(x => x.getStartVertex() == vertices[i] && x.getEndVertex() == vertices[j] ||
                                                       x.getEndVertex() == vertices[i] && x.getStartVertex() == vertices[j]).getWeight() + "\t";
                    }
                }
                res += "\n";
            }
            File.WriteAllTextAsync(fileName, res);
        }

        private static MathGraph OpenFromListOfEdges(string filename)
        {
            MathGraph mg = new MathGraph(filename.Remove(filename.Length-4));
            string[] str = File.ReadAllText(filename).Split("\n");
            int strLength = int.Parse(str[0]);
            string[] verticesName = str[1].Replace("(","").Replace(")", "").Split("\t");
            string[] strComponent;
            for (int i = 0; i < strLength; i++)
            {
                mg.AddVertex(verticesName[i]);
            }
            for(int i=2; i < str.Length-1; i++)
            {
                strComponent = str[i].Split(" ");
                Vertex vStart = mg.GetVertices().Find(x => x.GetName().Equals(strComponent[0]));
                Vertex vEnd = mg.GetVertices().Find(x => x.GetName().Equals(strComponent[1]));
                mg.AddEdge(vStart, vEnd, decimal.Parse(strComponent[2]));
            }
            return mg;
        }

        public static MathGraph OpenMathGraph()
        {
            MathGraph mg=null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                if (GetExtensionFile(filename)=="loe")
                {
                    mg = OpenFromListOfEdges(filename);
                }else
                    if (GetExtensionFile(filename) == "amx")
                {

                }
            }
            return mg;
        }
        private static string GetExtensionFile(string fileName)
        {
            return fileName.Substring(fileName.Length-3);
        }
        

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
namespace graphs
{
    public class Graph
    {
        private List<Button> Tops;
        private HashSet<(Button, Button)> Connections;
        
        public List<int> paths;
        public Graph()
        {
            Tops = new List<Button>();
            Connections = new HashSet<(Button, Button)>();
            paths = new List<int>();
        }

        public int GetConntTops() => Tops.Count;
        public Button GetTop() => Tops[0];
        public int GetConntConnections() => Connections.Count;
        public void AddTop(Button button) => Tops.Add(button);

        public void AddConnection(Button button_1, Button button_2)
        {
            if (Connections.Any(x => x.Item2 == button_1 && x.Item1 == button_2))
                return;
            Connections.Add((button_1, button_2));
        }
            
            
        public void RemoveTop(Button button)
        {
            Tops.Remove(button);
            Connections = Connections.Where(x => x.Item1 != button && x.Item2 != button).ToHashSet();
        }
            
        public void RemoveCConnection(Button button_1, Button button_2) => 
            Connections.RemoveWhere(x => x.Item1 == button_1 && x.Item2 == button_2 || x.Item1 == button_2 && x.Item2 == button_1);

        public string GetMatrix()
        {
            if (Tops.Count == 0)
                return string.Empty;

            var matrix = new int[Tops.Count + 1, Tops.Count + 1];
            string string_matrix = "0 ";

            Tops.ForEach(x => string_matrix += $"{x.Content} ");

            string_matrix += '\n';
            foreach (var point in Connections)
            {
                matrix[Tops.FindIndex(x => x == point.Item1), Tops.FindIndex(x => x == point.Item2)] = 1;
                matrix[Tops.FindIndex(x => x == point.Item2), Tops.FindIndex(x => x == point.Item1)] = 1;
            }
            for (int i = 0; i < Tops.Count; i++)
            {
                string_matrix += $"{Tops[i].Content} ";
                for (int j = 0; j < Tops.Count; j++)
                    string_matrix += $"{matrix[i, j]} ";

                string_matrix += '\n';
            }
            return string_matrix;
        }

        public string GetColumn()
        {
            string column = "";
            Connections.ToList().ForEach(x => column += $"{x.Item1.Content} {x.Item2.Content}\n");                
            return column;
        }


        public void dfs(int index_top, ref List<bool> used, ref List<List<int>> table)
        {
            Dictionary<Button, int> indexes = new Dictionary<Button, int>();
            var top = Tops[index_top];
            top.Background = Brushes.Black;
            System.Threading.Thread.Sleep(1000);
            paths.Add((int)top.Content);
            for (int i = 0; i < Tops.Count; i++)
                indexes[Tops[i]] = i;

            used[indexes[top]] = true;
            for (int i = 0; i < table[indexes[top]].Count; i++)
            {
                if (!used[table[indexes[top]][i]])
                {
                    dfs(table[indexes[top]][i], ref used, ref table);
                    paths.Add((int)top.Content);
                }
            }
        }
        public void DFS(Button top)
        {

            List<bool> used = new List<bool>(Tops.Count);
            for (int i = 0; i < Tops.Count; i++)
                used.Add(false);

            List<List<int>> table = new List<List<int>>();

            for (int i = 0; i < Tops.Count; i++)
            {
                table.Add(new List<int>());
            }

            Dictionary<Button, int> indexes = new Dictionary<Button, int>();

            for (int i = 0; i < Tops.Count; i++)
                indexes[Tops[i]] = i;

            foreach (var v in Connections)
            {
                table[indexes[v.Item1]].Add(indexes[v.Item2]);
                table[indexes[v.Item2]].Add(indexes[v.Item1]);
            }
            dfs(indexes[top], ref used, ref table);
        }
    }
}

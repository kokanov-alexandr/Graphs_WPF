using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace graphs
{
    public class Graph
    {
        private List<Button> Tops;
        private HashSet<(Button, Button)> Connections;

        public Graph()
        {
            Tops = new List<Button>();
            Connections = new HashSet<(Button, Button)>();
        }

        public int GetConntTops() => Tops.Count;
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
    }
}

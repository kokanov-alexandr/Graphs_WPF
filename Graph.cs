using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace graphs
{
    public struct Connection
    {
        public Button button_1;
        public Button button_2;
        public Line line;

        public Connection(Button button_1, Button button_2, Line line)
        {
            this.button_1 = button_1;
            this.button_2 = button_2;
            this.line = line;
        }
    }
    public class Graph
    {
        public List<Button> Tops;

        public List<Connection> Connections;

        private Dictionary<Button, int> Indexes;

        public List<Button> Paths;

        private List<List<int>> Table;

        private List<bool> Used;

        public Graph()
        {
            Tops = new List<Button>();

            Connections = new List<Connection>();
        }


        public void AddConnection(Button button_1, Button button_2, Line line)
        {
            if (Connections.Any(x => x.button_2 == button_1 && x.button_1 == button_2 || x.button_1 == button_1 && x.button_2 == button_2))
                return;
            Connections.Add(new Connection(button_1, button_2, line));
        }
            
        public void RemoveTop(Button button)
        {
            Tops.Remove(button);
            var ConnecCopy = Connections.ToList();
            for (int i = 0; i < ConnecCopy.Count; i++)
            {
                if (ConnecCopy[i].button_1 == button || ConnecCopy[i].button_2 == button)
                {
                    Connections.Remove(ConnecCopy[i]);
                    ((MainWindow)System.Windows.Application.Current.MainWindow).grid.Children.Remove(ConnecCopy[i].line);
                }
            }
        }
            
        public void RemoveConnection(Button button_1, Button button_2)
        {
            var ConnecCopy = Connections.ToList();
            for (int i = 0; i < ConnecCopy.Count; i++)
            {
                if (ConnecCopy[i].button_1 == button_1 && ConnecCopy[i].button_2 == button_2 || ConnecCopy[i].button_1 == button_2 && ConnecCopy[i].button_2 == button_1)
                {
                    Connections.Remove(ConnecCopy[i]);
                    ((MainWindow)System.Windows.Application.Current.MainWindow).grid.Children.Remove(ConnecCopy[i].line);
                }
            }
        }

        public string GetMatrix()
        {
            var matrix = new int[Tops.Count + 1, Tops.Count + 1];

            string string_matrix = "0 ";

            Tops.ForEach(x => string_matrix += $"{x.Content} ");

            string_matrix += '\n';

            foreach (var point in Connections)
            {
                matrix[Tops.FindIndex(x => x == point.button_1), Tops.FindIndex(x => x == point.button_2)] = 1;
                matrix[Tops.FindIndex(x => x == point.button_2), Tops.FindIndex(x => x == point.button_1)] = 1;
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
            Connections.ToList().ForEach(x => column += $"{x.button_1.Content} {x.button_2.Content}\n");
            return column;
        }

        public void InitBaseDFS()
        {
            Paths = new List<Button>();

            Indexes = new Dictionary<Button, int>();

            for (int i = 0; i < Tops.Count; i++)
                Indexes[Tops[i]] = i;

            Table = new List<List<int>>();

            for (int i = 0; i < Tops.Count; i++)
                Table.Add(new List<int>());

            foreach (var v in Connections)
            {
                Table[Indexes[v.button_1]].Add(Indexes[v.button_2]);
                Table[Indexes[v.button_2]].Add(Indexes[v.button_1]);
            }

            Used = new List<bool>();

            foreach (var v in Tops)
                Used.Add(false);
        }

        public void ClearBaseDFS()
        {
            Paths.Clear();

            Indexes.Clear();

            Table.Clear();

            Used.Clear();
        }

        public void Dfs(Button top)
        {

            if (!Used[Indexes[top]])
                Paths.Add(top);
            Used[Indexes[top]] = true;

            for (int i = 0; i < Table[Indexes[top]].Count; i++)
            {
                if (!Used[Table[Indexes[top]][i]])
                {
                    Dfs(Tops[Table[Indexes[top]][i]]);
                }
            }
        }
    }
}

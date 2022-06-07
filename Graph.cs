using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace graphs
{
    public struct Connection
    {
        public Button button_1;
        public Button button_2;
        public Shape figure;

        public Connection(Button button_1, Button button_2, Shape figure)
        {
            this.button_1 = button_1;
            this.button_2 = button_2;
            this.figure = figure;
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

        public void AddTop(Button button)
        {
            ((MainWindow)Application.Current.MainWindow).grid.Children.Add(button);
            Tops.Add(button);
        }

        public void AddConnection(Button button_1, Button button_2)
        {
            var grid = ((MainWindow)Application.Current.MainWindow).grid;
            if (Connections.Any(x => x.button_2 == button_1 && x.button_1 == button_2 || x.button_1 == button_1 && x.button_2 == button_2))
                return;

            Shape NewFigure;

            if (button_1 == button_2)
            {
                NewFigure = CreateMyElements.CreateEllipse(new Point(button_1.Margin.Left + Constants.TopSize / 2, button_1.Margin.Top));     
            }
            else
            {
                NewFigure = CreateMyElements.CreateLine(new Point(button_1.Margin.Left + Constants.TopSize / 2, button_1.Margin.Top + Constants.TopSize / 2),
                    new Point(button_2.Margin.Left + Constants.TopSize / 2, button_2.Margin.Top + Constants.TopSize / 2));
            }
            grid.Children.Add(NewFigure);
            Connections.Add(new Connection(button_1, button_2, NewFigure));
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
                    ((MainWindow)Application.Current.MainWindow).grid.Children.Remove(ConnecCopy[i].figure);
                }
            }
            ((MainWindow)Application.Current.MainWindow).grid.Children.Remove(button);
        }
            
        public void RemoveConnection(Button button_1, Button button_2)
        {
            var ConnecCopy = Connections.ToList();
            for (int i = 0; i < ConnecCopy.Count; i++)
            {
                if (ConnecCopy[i].button_1 == button_1 && ConnecCopy[i].button_2 == button_2 || ConnecCopy[i].button_1 == button_2 && ConnecCopy[i].button_2 == button_1)
                {
                    Connections.Remove(ConnecCopy[i]);
                    ((MainWindow)Application.Current.MainWindow).grid.Children.Remove(ConnecCopy[i].figure);
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

        public void InitBaseSearch()
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

        public void Dfs(Button top)
        {
            InitBaseSearch();
            var stack = new Stack<Button>();
                
            Used[Indexes[top]] = true;
            stack.Push(top);
            while (stack.Count > 0)
            {
                var y = stack.Pop();
                Paths.Add(y);
                for (int i = 0; i < Table[Indexes[y]].Count; i++)
                {
                    if (!Used[Table[Indexes[y]][i]])
                    {
                        Used[Table[Indexes[y]][i]] = true;
                        stack.Push(Tops[Table[Indexes[y]][i]]);
                    }
                }
            }
        }

        public void Bfs(Button top)
        {
            InitBaseSearch();
            var queue = new Queue<Button>();

            Paths.Add(top);
            Used[Indexes[top]] = true;
            queue.Enqueue(top);
            while (queue.Count > 0)
            {
                var y = queue.Dequeue();
                for (int i = 0; i < Table[Indexes[y]].Count; i++)
                {
                    if (!Used[Table[Indexes[y]][i]])
                    {
                        Used[Table[Indexes[y]][i]] = true;
                        Paths.Add(Tops[Table[Indexes[y]][i]]);
                        queue.Enqueue(Tops[Table[Indexes[y]][i]]);
                    }
                }
            }
        }

    }
}

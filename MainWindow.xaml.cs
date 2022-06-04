using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace graphs
{
    public partial class MainWindow : Window
    {
        private Button button;
        private readonly DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            ActiveButtons();
            timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 1) };
            timer.Tick += TimerTick;
        }
        private Controller controler = new Controller();
        private Graph graph = new Graph();


        private void ActiveButtons()
        {
            int counter_tops = graph.Tops.Count;
            int counter_connections = graph.Connections.Count;
            btn_delete_top.IsEnabled = counter_tops > 0;
            btn_add_connection.IsEnabled = counter_tops > 0;
            btn_delete_connection.IsEnabled = counter_connections > 0;
            BtnGetСColumn.IsEnabled = counter_connections > 0;
            BtnGetMatrix.IsEnabled = counter_connections > 0;
            BFS.IsEnabled = counter_tops > 0;
            DeleteAll.IsEnabled = counter_tops > 0;
        }

        private void GridPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (controler.IsBtnAddTopClick == Controller.State.Active)
            {
                Point upPoint = e.GetPosition(grid);
                if (upPoint.X < Constants.MenuBorder || upPoint.X > grid.ActualWidth ||
                   upPoint.Y < 0 || upPoint.Y > grid.ActualHeight || upPoint.X > Constants.HelpButtonBorderX && upPoint.Y > Constants.HelpButtonBorderY)
                    return;

                Button NewTop =  CreateMyElements.CreateButton(new Point(upPoint.X - Constants.TopSize / 2, upPoint.Y - Constants.TopSize / 2));
                NewTop.PreviewMouseDown += TopMouseLeftButtonDown;
                graph.AddTop(NewTop);
                ActiveButtons();
            }

        }

        private void TopMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            if (controler.IsBtnDeleteTopClick == Controller.State.Active)
            {
                graph.RemoveTop(sender as Button);
            }

            else if (controler.IsBtnAddConnectionClick == Controller.State.Active)
            {
                controler.IsBtnAddConnectionClick = Controller.State.Active_2;
                button = sender as Button;
            }

            else if (controler.IsBtnAddConnectionClick == Controller.State.Active_2)
            {
                graph.AddConnection(button, sender as Button);
                controler.IsBtnAddConnectionClick = Controller.State.Active;
                button = null;
            }

            else if (controler.IsBtnDeleteConnectionClick == Controller.State.Active)
            {
                controler.IsBtnDeleteConnectionClick = Controller.State.Active_2;
                button = sender as Button;
            }

            else if (controler.IsBtnDeleteConnectionClick == Controller.State.Active_2)
            {
                graph.RemoveConnection(button, sender as Button);
                button = null;
                controler.IsBtnDeleteConnectionClick = Controller.State.Active;
            }
                
            else if (controler.IsDFSBtnClick == Controller.State.Active)
            {
                controler.IsDFSBtnClick = Controller.State.Inactive;
                graph.InitBaseDFS();
                graph.Dfs(sender as Button);
                timer.Start();  
            }
            else
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    (sender as Button).Content = e.ClickCount == 1 ? ((int)(sender as Button).Content + 1) : ((int)(sender as Button).Content + 9);
                if(e.RightButton == MouseButtonState.Pressed) 
                    (sender as Button).Content = e.ClickCount == 1 ? ((int)(sender as Button).Content - 1) : ((int)(sender as Button).Content - 9);
            }
            ActiveButtons();
        }

        private int increment = 0;
        private void TimerTick(object sender, object e)
        {
            if (increment == graph.Paths.Count + 1)
            {
                foreach (var path in graph.Paths) 
                    path.Background = Brushes.Blue;
                increment = 0;
                timer.Stop();
                return;
            }

            else if (increment == graph.Paths.Count)
                graph.Paths[increment - 1].Background = Brushes.DarkOrange;

            else
            {
                graph.Paths[increment].Background = Brushes.DarkOrange;
                if (increment > 0)
                    graph.Paths[increment - 1].Background = Brushes.LightCoral;
            }

            increment++;   
        }


        private void BtnAddTopClick(object sender, RoutedEventArgs e) =>
            controler.CheckStatus(ref controler.IsBtnAddTopClick, sender as Button);

        private void BtnDeleteTopClick(object sender, RoutedEventArgs e) =>
            controler.CheckStatus(ref controler.IsBtnDeleteTopClick, sender as Button);

        private void BtnDeleteConnectionClick(object sender, RoutedEventArgs e) =>
            controler.CheckStatus(ref controler.IsBtnDeleteConnectionClick, sender as Button);

        private void BtnAddConnectionClick(object sender, RoutedEventArgs e) =>
            controler.CheckStatus(ref controler.IsBtnAddConnectionClick, sender as Button);


        private void BtnGetMatrixClick(object sender, RoutedEventArgs e)
        {
            controler.ClearInfo();
            string matrix_string = graph.GetMatrix();
            MessageBoxResult result = MessageBox.Show(matrix_string, "Скопировать в буфер обмена?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes) 
                Clipboard.SetText(matrix_string);
        }

        private void BtnGetСColumnClick(object sender, RoutedEventArgs e)
        {
            controler.ClearInfo();
            string column_string = graph.GetColumn();
            MessageBoxResult result = MessageBox.Show(column_string, "Скопировать в буфер обмена?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                Clipboard.SetText(column_string);
        }

        private void DFSClick(object sender, RoutedEventArgs e)
        {
            controler.ClearInfo();
            controler.IsDFSBtnClick = Controller.State.Active;
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            Help help_win = new Help
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            help_win.Show();
        }

        private void DeleteAllClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите удалить весь граф?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            graph.Tops.ForEach(x => grid.Children.Remove(x));
            graph.Tops.Clear();
            graph.Connections.ToList().ForEach(x => grid.Children.Remove(x.figure));
            graph.Connections.Clear();
            ActiveButtons();
        }
    }
}

        


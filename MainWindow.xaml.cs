using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace graphs
{

    public partial class MainWindow : Window
    {
        private Button button;
        public MainWindow()
        {
            InitializeComponent();
        }

        private Controler controler = new Controler();
        private Graph graph = new Graph();


        private void Grid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (controler.IsBtnAddTopClick == Controler.State.Second)
            {
                Point upPoint = e.GetPosition(grid);
                if (upPoint.X < 180 || upPoint.X > grid.ActualWidth ||
                   upPoint.Y < 0 || upPoint.Y > grid.ActualHeight)
                    return;

                Button button = new Button
                {
                    Margin = new Thickness(upPoint.X - 20, upPoint.Y - 20, 0, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Background = new SolidColorBrush(Colors.Blue),
                    Content = grid.Children.Count - 5,
                    Width = 47,
                    Height = 47,
                    FontSize = 9
                };
                button.PreviewMouseDown += BtnTopMouseLeftButtonDown;
                grid.Children.Add(button);
                graph.AddTop(button);   
                controler.ClearInfo();
            }

        }

        private void BtnTopMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (controler.IsBtnDeleteTopClick == Controler.State.Second)
            {
                graph.RemoveTop(sender as Button);
                grid.Children.Remove(sender as Button);
                controler.ClearInfo();
            }

            else if (controler.IsBtnAddConnectionClick == Controler.State.Second)
            {
                controler.IsBtnAddConnectionClick = Controler.State.Third;
                button = sender as Button;
            }

            else if (controler.IsBtnAddConnectionClick == Controler.State.Third)
            {
                graph.AddConnection(button, sender as Button);
                button = null;
                controler.IsBtnAddConnectionClick = Controler.State.First;
            }

            else if (controler.IsBtnDeleteConnectionClick == Controler.State.Second)
            {
                controler.IsBtnDeleteConnectionClick = Controler.State.Third;
                button = sender as Button;
            }

            else if (controler.IsBtnDeleteConnectionClick == Controler.State.Third)
            {
                graph.RemoveCConnection(button, sender as Button);
                button = null;
                controler.IsBtnDeleteConnectionClick = Controler.State.First;
            }
                
            else
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    (sender as Button).Content = e.ClickCount == 1 ? ((int)(sender as Button).Content + 1) : ((int)(sender as Button).Content + 10);
                if(e.RightButton == MouseButtonState.Pressed) 
                    (sender as Button).Content = e.ClickCount == 1 ? ((int)(sender as Button).Content - 1) : ((int)(sender as Button).Content - 10);
            }

        }

        private void Btn_add_top_Click(object sender, RoutedEventArgs e)
        {
            controler.ClearInfo();
            controler.IsBtnAddTopClick = Controler.State.Second;
        }   

        private void Btn_delete_top_Click(object sender, RoutedEventArgs e)
        {
            controler.ClearInfo();
            controler.IsBtnDeleteTopClick = Controler.State.Second;
        }

        private void Btn_delete_connection_Click(object sender, RoutedEventArgs e)
        {
            controler.ClearInfo();
            controler.IsBtnDeleteConnectionClick = Controler.State.Second;
        }

        private void Btn_add_connection_Click(object sender, RoutedEventArgs e)
        {
            controler.ClearInfo();
            controler.IsBtnAddConnectionClick = Controler.State.Second;
        }

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

    }
}

        


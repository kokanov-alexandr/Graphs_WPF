using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace graphs
{
    public static class CreateMyElements
    {

        public static Button CreateButton(Point cords)
        {
            var MyWin = ((MainWindow)Application.Current.MainWindow);
            Button button = new Button
            {
                Margin = new Thickness(cords.X, cords.Y, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = new SolidColorBrush(Colors.Blue),
                Content = MyWin.grid.Children.Count - Constants.CountMenuButtons + 1,
                Width = Constants.TopSize,
                Height = Constants.TopSize,
                FontSize = Constants.TopFontSize,
                Foreground = new SolidColorBrush(Colors.Black),
                FontStretch = new FontStretch(),
                Style = (Style)MyWin.Resources["ForRadius"]
            };
            return button;
        }

        public static Ellipse CreateEllipse(Point cords)
        {
            Ellipse ellipse = new Ellipse
            {
                Width = 50,
                Height = 40,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = Constants.LineThickness,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(cords.X, cords.Y, 0, 0)
            };
            return ellipse;
        }

        public static Line CreateLine(Point cords_1, Point cords_2)
        {
            Line line = new Line
            {
                X1 = cords_1.X,
                Y1 = cords_1.Y,
                X2 = cords_2.X,
                Y2 = cords_2.Y,
                StrokeThickness = Constants.LineThickness,
                Stroke = Brushes.Black,
            };
            return line;
        }
    }
}

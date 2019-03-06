using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ComplexFigure.Model
{
    static class Vertex
    {
        public static Ellipse CreateDot(int radius, SolidColorBrush color, Point coordinates)
        {
            var dot = new Ellipse
            {
                Width = radius,
                Height = radius,
                Fill = color,
                Stroke = Brushes.LightGreen
            };

            Canvas.SetLeft(dot, coordinates.X);
            Canvas.SetTop(dot, coordinates.Y);

            return dot;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BrownianMotion
{
    static class Spot
    {
        public static Ellipse CreateDot(int radius, SolidColorBrush color, Point coordinates)
        {
            var dot = new Ellipse {Width = radius, Height = radius, Fill = color};

            Canvas.SetLeft(dot, coordinates.X);
            Canvas.SetTop(dot, coordinates.Y);

            return dot;
        }

    }
}

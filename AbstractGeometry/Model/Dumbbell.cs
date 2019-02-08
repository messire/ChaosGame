using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AbstractGeometry.Model
{
    class Dumbbell
    {
        private int _length;
        private double _radius;
        private readonly Random _rnd = new Random();


        public Ellipse FirstDot { get; set; }
        public Ellipse SecondDot { get; set; }
        public Line Link { get; set; }

        public Dumbbell(Point clickPoint)
        {
            _radius = 5;
            _length = 50;

            FirstDot = new Ellipse
            {
                Width = _radius,
                Height = _radius,
                Fill = Brushes.LightGreen,
                Stroke = Brushes.ForestGreen
            };
            
            SecondDot = new Ellipse
            {
                Width = _radius,
                Height = _radius,
                Fill = Brushes.LightGreen,
                Stroke = Brushes.ForestGreen
            };

            //TODO переписать еботу
            int brPoint = _rnd.Next(1, _length);
            int ax = _rnd.Next(1, brPoint);
            double x1 = clickPoint.X - ax;
            double y1 = clickPoint.Y - Math.Sqrt(Math.Pow(brPoint, 2) - Math.Pow(ax, 2));

            var cos = brPoint / Math.Sqrt(Math.Pow(brPoint, 2) - Math.Pow(ax, 2));
            double a = cos * _length;

            double y2 = y1 - a;
            double x2 = Math.Sqrt(Math.Pow(_length, 2) - Math.Pow(a, 2));

            //----------------------

            Link = new Line
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Fill = Brushes.ForestGreen,
                Width = 2
            };

            Canvas.SetLeft(FirstDot, clickPoint.X);
            Canvas.SetTop(FirstDot, clickPoint.Y);
            Canvas.SetLeft(SecondDot, clickPoint.X);
            Canvas.SetTop(SecondDot, clickPoint.Y);
        }
    }
}

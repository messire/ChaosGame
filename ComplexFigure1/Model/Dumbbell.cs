using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Shapes;

namespace ComplexFigure.Model
{
    class Dumbbell
    {
        private int _length;
        private double _radius;
        private double _cos;
        private double _sin;
        private readonly Random _rnd = new Random();


        public Ellipse FirstDot { get; set; }
        public Ellipse SecondDot { get; set; }
        public Line Link { get; set; }

        private double Angle
        {
            set
            {
                _cos = Math.Cos(value * Math.PI / 180);
                _sin = Math.Sin(value * Math.PI / 180);
            }
        }

        public Dumbbell(Point clickPoint)
        {
            InitGeometry();
            GetPoints(clickPoint, out Point pointA, out Point pointB);

            FirstDot = Vertex.CreateDot((int) _radius, Brushes.ForestGreen, pointA);
            SecondDot = Vertex.CreateDot((int) _radius, Brushes.ForestGreen, pointB);

            //----------------------

            Link = new Line
            {
                X1 = pointA.X + _radius / 2,
                Y1 = pointA.Y + _radius / 2,
                X2 = pointB.X + _radius / 2,
                Y2 = pointB.Y + _radius / 2,
                Stroke = Brushes.ForestGreen,
            };
        }

        public void SetRadius(double radius) => _radius = radius;

        private void InitGeometry()
        {
            _radius = 10;
            _length = _rnd.Next(2, 200);
            Angle = _rnd.Next(0, 180);
        }

        private void GetPoints(Point clickPoint, out Point pointA, out Point pointB)
        {
            int breakPoint = _rnd.Next(2, _length);
            int line2Length = _length - breakPoint;

            double a = clickPoint.X - _cos * breakPoint;
            double b = clickPoint.Y - _sin * breakPoint;
            double c = clickPoint.X + _cos * line2Length;
            double d = clickPoint.Y + _sin * line2Length;

            if (a < 0) a = 0;
            if (b < 0) b = 0;
            if (c < 0) c = 0;
            if (d < 0) d = 0;

            pointA = new Point(a, b);
            pointB = new Point(c, d);
        }
    }
}

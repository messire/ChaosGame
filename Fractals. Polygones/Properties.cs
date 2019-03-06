using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fractals.Polygones
{
    class Props
    {
        #region Fields

        private Point _center;
        private int _divider;
        private readonly int _dotSize;
        private double _height;
        private int _vertexes;
        private double _width;
        private readonly Random _rnd;
        private double _speed;
        private readonly List<Dot> _pointList;

        #endregion

        #region Properties

        public int Divider => _divider;
        public List<Dot> PointList => _pointList;
        public double Speed => _speed;
        public int Vertexes => _vertexes;

        private double Height
        {
            get => _height;
            set
            {
                _height = value;
                _center = new Point(_width / 2, _height / 2);
            }
        }

        private double Width
        {
            get => _width;
            set
            {
                _width = value;
                _center = new Point(Width / 2, Height / 2);
            }
        }

        #endregion

        #region Constructor

        private Props()
        {
            _center = new Point();
            _height = 0;
            _width = 0;
            _pointList = new List<Dot>();
            _rnd = new Random();
        }

        public Props(int dotSize, FrameworkElement form) : this() => SetParams(form);

        #endregion

        #region Methods

        public void ClearPointList() => PointList.Clear();

        public void GenerateVertex()
        {
            for (int i = 0; i < _vertexes; i++)
            {
                double x = _center.X + (_center.Y - 30) * Math.Sin(i * 2 * Math.PI / _vertexes);
                double y = _center.Y + (_center.Y - 30) * Math.Cos(i * 2 * Math.PI / _vertexes);

                _pointList.Add(new Dot(new Point(x, y), 0));
            }
        }

        public Point GetRandomVertex()
        {
            int index = _rnd.Next(0, _vertexes);
            PointList[index].Count++;

            return PointList[index].Point;
        }

        public void SetParams(FrameworkElement form)
        {
            Height = form.ActualHeight - 80;
            Width = form.ActualWidth;
        }

        public void SetSpeed(int value) => _speed = Math.Exp((double) value);
        
        public void SetVertexCount(int value)
        {
            _vertexes = value;
            _divider = _vertexes - 1;
        }

        #endregion
    }
}

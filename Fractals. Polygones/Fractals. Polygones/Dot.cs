using System.Windows;

namespace Fractals.Polygones
{
    class Dot
    {
        public Point Point { get; set; }
        public int Count { get; set; }

        public Dot(Point point, int count)
        {
            Point = point;
            Count = count;
        }
    }
}

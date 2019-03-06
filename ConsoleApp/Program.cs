using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleApp
{
    class Program
    {
        class Point
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        static void Main(string[] args)
        {
            var aKey = new ConsoleKeyInfo();
            var random = new Random();
            int length = 50;

            while (aKey.Key != ConsoleKey.Escape)
            {
                var angle = random.Next(0, 90);
                var cos = Math.Cos(angle * Math.PI / 180);
                var sin = Math.Sin(angle * Math.PI / 180);

                var clickPoint = new Point(random.Next(0, 800), random.Next(0, 400));

                var breakPoint = random.Next(2, length);
                var line1Length = breakPoint;

                var a = sin * line1Length;
                var b = cos * line1Length;

                var pointA = new Point(clickPoint.X - b, clickPoint.Y - a);

                Console.Clear();
                Console.WriteLine($"length: {length}");
                Console.WriteLine($"angle: {angle}");
                Console.WriteLine($"cos: {cos}");
                Console.WriteLine($"cos: {sin}");
                Console.WriteLine($"clickPoint: [ {clickPoint.X}, {clickPoint.Y} ]");
                Console.WriteLine($"breakPoint: {breakPoint}");
                Console.WriteLine($"lenth of line 1: {line1Length}");
                Console.WriteLine($"katet a: {a}");
                Console.WriteLine($"katet b: {b}");
                Console.WriteLine($"Point A: [ {pointA.X}, {pointA.Y} ]");

                aKey = Console.ReadKey();
            }
        }
    }
}

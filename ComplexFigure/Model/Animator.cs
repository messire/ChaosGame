using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace ComplexFigure.Model
{
    static class Animator
    {
        private static readonly Random Rnd = new Random();

        public static DoubleAnimation CreateDoubleAnimation(double? beginPoint, double endPoint) =>
            new DoubleAnimation {From = beginPoint, To = GetToPoint((int) endPoint), Duration = GetDuration()};

        public static void ApplyAnimation(DependencyObject obj, DoubleAnimation da, PropertyPath pp)
        {
            Storyboard.SetTarget(da, obj);
            Storyboard.SetTargetProperty(da, pp);
        }

        private static TimeSpan GetDuration() => TimeSpan.FromMilliseconds(Rnd.Next(1000, 2000));

        private static double GetToPoint(int maxValue) => Rnd.Next(maxValue);
    }
}

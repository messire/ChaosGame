using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BrownianMotion
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var beginPoint = e.GetPosition(null);

            var ellipse = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Black
            };

            DrawCanvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, beginPoint.X);
            Canvas.SetTop(ellipse, beginPoint.Y);

            Storyboard story = new Storyboard();

            var endPoint = new Point(beginPoint.X + 450, beginPoint.Y + 50);

            var doubleAnimation = new DoubleAnimation
            {
                From = beginPoint.X,
                To = endPoint.X,
                Duration = TimeSpan.FromSeconds(4)
            };
            
            //dax.Completed += (sender, args) => { //animate again };

            Storyboard.SetTarget(doubleAnimation, ellipse);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Canvas.LeftProperty));

            story.Children.Add(doubleAnimation);
            story.Begin();

        }
    }
}

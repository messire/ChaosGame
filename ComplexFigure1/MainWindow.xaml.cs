using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ComplexFigure.Model;

namespace ComplexFigure
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _length = 100;
        private int _radius = 5;

        private readonly Random _random = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var d = new Dumbbell(e.GetPosition(this));

            DrawCanvas.Children.Add(d.FirstDot);
            DrawCanvas.Children.Add(d.SecondDot);
            DrawCanvas.Children.Add(d.Link);
        }
    }
}

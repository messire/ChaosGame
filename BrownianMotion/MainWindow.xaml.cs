using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
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
        private readonly DrawData _drawData;

        public MainWindow()
        {
            InitializeComponent();
            _drawData = new DrawData();
            SizeChanged += UpdateDrawDataFormSize;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DoMagic(e.GetPosition(this));
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            //DoMagic(e.GetPosition(this));
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DrawCanvas.Children.Clear();
        }

        private void DoMagic(Point clickPoint)
        {
            if (DrawCanvas.Children.Count > 4) DrawCanvas.Children.RemoveAt(0);

            _drawData.SetLastClick(clickPoint);
            _drawData.Init();
            DrawCanvas.Children.Add(_drawData.Dot);
        }

        private void UpdateDrawDataFormSize(object sender, SizeChangedEventArgs e) => _drawData.FormSizeChange(new Point(this.ActualWidth, this.ActualHeight));
    }
}

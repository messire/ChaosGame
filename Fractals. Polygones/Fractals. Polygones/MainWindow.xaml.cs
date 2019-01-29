using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
using System.Xml.Serialization;
using static System.Char;

namespace Fractals.Polygones
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Props _props;
        private bool _running = false;
        private int _speed;

        private const int VertexPointSize = 10;
        private const int BasePointSize = 5;
        private const int ChaosPointSize = 2;

        private static readonly Brush PickedPoint = Brushes.Green;
        private static readonly Brush ChaosPoint = Brushes.Black;
        private static readonly Brush VertexPoint = Brushes.Red;
        
        #region attractor variables

        private int _iterationCount = 10000;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            _props = new Props(2, this);
            SetSpeedValue();
            SetVertexCount();
            _props.GenerateVertex();
        }

        #region Control handlers

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (_running)
            {
                Stop();
                return;
            }

            Start();
        }

        private void SpeedBox_TextChanged(object sender, TextChangedEventArgs e) => SetSpeedValue();

        private void SpeedBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => CheckInput(e);

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => SpeedBox.Text = Math.Round(e.NewValue, 0).ToString();

        private void VertexBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => CheckInput(e);

        private void VertexBox_TextChanged(object sender, TextChangedEventArgs e) => DrawPolygon();

        private void VertexSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => VertexBox.Text = Math.Round(e.NewValue, 0).ToString();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrawPolygon();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point pick = CalibratePoint(e.GetPosition(DrawPanel), BasePointSize);
            DrawClickPoint(pick);
        }

        private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClearDeck();
        }
        
        #endregion

        private void DoChaos()
        {
            UIElement pickedPoint = FindPickedPoint();
            if (pickedPoint == null)
            {
                Stop();
                return;
            }

            var pick = new Point(Canvas.GetTop(pickedPoint), Canvas.GetLeft(pickedPoint));
            new Thread(
                    () =>
                    {
                        for (int i = 0; i < _iterationCount; i++)
                        {
                            if (!_running) break;
                            pick = Iterate(pick);
                            Thread.Sleep((int) (1000 / _props.Speed));
                        }
                    }
                )
                {IsBackground = true}.Start();
        }

        private Point Iterate(Point activePoint)
        {
            Point vertex = _props.GetRandomVertex();

            activePoint = new Point(GetCoordValue(vertex.X, activePoint.X), GetCoordValue(vertex.Y, activePoint.Y));
            Dispatcher.Invoke(() => { DrawChaosDot(activePoint); });

            return activePoint;
        }

        #region Drawing

        private void DrawChaosDot(Point coord) => PutDotToCanvas(CreateEllipse(ChaosPoint, ChaosPointSize), coord);

        private void DrawClickPoint(Point coord)
        {
            var pickedPoint = FindPickedPoint();
            DrawPanel.Children.Remove(pickedPoint);

            PutDotToCanvas(CreateEllipse(PickedPoint, BasePointSize), coord);
        }

        private void DrawVertex(Point coord) => PutDotToCanvas(CreateEllipse(VertexPoint, VertexPointSize), CalibratePoint(coord, VertexPointSize));

        private void PutDotToCanvas(Ellipse dot, Point coord)
        {
            DrawPanel.Children.Add(dot);
            Canvas.SetLeft(dot, coord.X);
            Canvas.SetTop(dot, coord.Y);
        }

        #endregion

        #region Helpers

        private Point CalibratePoint(Point pick, int pointSize) => new Point(pick.X - (double)pointSize / 2, pick.Y - (double)pointSize / 2);

        private bool CheckInput(TextCompositionEventArgs input) => input.Handled = !IsDigit(input.Text, 0);

        private void ClearDeck()
        {
            if (DrawPanel == null) return;
            DrawPanel.Children.Clear();
            _props.ClearPointList();
            _props.SetParams(this);
        }

        private void ClearChaos()
        {
            List<UIElement> list = new List<UIElement>();
            foreach (UIElement child in DrawPanel.Children)
            {
                if (!(child is Ellipse ellipse)) continue;
                if (ellipse.Fill != ChaosPoint) continue;

                list.Add(child);
                break;
            }

            list.ForEach(e => DrawPanel.Children.Remove(e));
        }

        private Ellipse CreateEllipse(Brush dotColor, double dotSize) => new Ellipse { Height = dotSize, Width = dotSize, Fill = dotColor };

        private void DrawPolygon()
        {
            if (_props == null) return;
            Stop();
            ClearDeck();
            SetSpeedValue();
            SetVertexCount();
            _props.GenerateVertex();
            _props.PointList.ForEach(p => DrawVertex(p.Point));
        }

        private UIElement FindPickedPoint()
        {
            return FindPoint(PickedPoint);
        }

        private UIElement FindPoint(Brush brush)
        {
            UIElement result = null;
            foreach (UIElement child in DrawPanel.Children)
            {
                if (!(child is Ellipse ellipse)) continue;
                if (ellipse.Fill != brush) continue;

                result = child;
                break;
            }

            return result;
        }

        private double GetCoordValue(double vertex, double dot) => vertex > dot ? vertex - (vertex - dot) / _props.Divider : vertex + (dot - vertex) / _props.Divider;

        private void SetSpeedValue()
        {
            if (SpeedBox == null || _props == null) return;

            int.TryParse(SpeedBox.Text, out int value);
            _props.SetSpeed(value);
        }

        private void SetVertexCount()
        {
            if (VertexBox == null || _props == null) return;

            int.TryParse(VertexBox.Text, out int value);
            _props.SetVertexCount(value);
        }

        private void Start()
        {
            _running = true;
            PlayButton.Content = ";";
            ClearChaos();
            DoChaos();
        }

        private void Stop()
        {
            _running = false;
            PlayButton.Content = "4";
        }

        #endregion
    }
}

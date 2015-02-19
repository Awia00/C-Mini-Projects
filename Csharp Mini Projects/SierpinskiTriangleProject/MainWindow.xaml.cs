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

namespace SierpinskiTriangleProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int depth = 5;
        private SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(255,255,255));
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewTriangle(PointCollection newTriangle, int currentDepth)
        {
            currentDepth++;
            if (depth >= currentDepth)
            {
                DrawInverseTriangle(newTriangle);
                double newLength = (newTriangle[1].X - newTriangle[0].X)/2;
                PointCollection next1Triangle = new PointCollection(new List<Point>()
                {
                    new Point(newTriangle[0].X, newTriangle[0].Y),
                    new Point(newTriangle[1].X-newLength, newTriangle[1].Y),
                    new Point(newTriangle[2].X-newLength/2, newTriangle[2].Y+newLength),
                });
                NewTriangle(next1Triangle,currentDepth);

                PointCollection next2Triangle = new PointCollection(new List<Point>()
                {
                    new Point(newTriangle[0].X+newLength, newTriangle[0].Y),
                    new Point(newTriangle[1].X, newTriangle[1].Y),
                    new Point(newTriangle[2].X+newLength/2, newTriangle[2].Y+newLength),
                });
                NewTriangle(next2Triangle,currentDepth);

                PointCollection next3Triangle = new PointCollection(new List<Point>()
                {
                    new Point(newTriangle[0].X+newLength/2, newTriangle[0].Y-newLength),
                    new Point(newTriangle[1].X-newLength/2, newTriangle[1].Y-newLength),
                    new Point(newTriangle[2].X-newLength/2, newTriangle[2].Y),
                });
                NewTriangle(next3Triangle, currentDepth);
            }
        }

        private void DrawInverseTriangle(PointCollection newTriangle)
        {
            double newLength = (newTriangle[1].X - newTriangle[0].X)/2;
            PointCollection secondTriangle = new PointCollection(new List<Point>()
            {
                new Point(newTriangle[0].X+newLength, newTriangle[0].Y),
                new Point(newTriangle[0].X+newLength/2, newTriangle[0].Y-newLength),
                new Point(newTriangle[0].X+newLength+newLength/2, newTriangle[0].Y-newLength),
            });
            Canvas.Children.Add(new Polygon()
            {
                Fill = brush,
                Points = secondTriangle,
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double height = Canvas.ActualHeight;
            int.TryParse(DepthTextBox.Text, out depth);
            Canvas.Children.Clear();
            PointCollection originalTriangle = new PointCollection(new List<Point>()
            {
                new Point(0, height),
                new Point(height, height),
                new Point(height/2, 0),
            });
            Canvas.Children.Add(new Polygon()
            {
                Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                Points = originalTriangle,
            });

            NewTriangle(originalTriangle, 0);
        }
    }
}

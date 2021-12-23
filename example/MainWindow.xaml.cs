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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Line newLine = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = (Point)e.GetPosition(canvas1);

            newLine = new Line();
            newLine.X1 = clickPoint.X;
            newLine.Y1 = clickPoint.Y;
            newLine.Fill = Brushes.Green;
            newLine.Stroke = Brushes.Blue;

            Canvas.SetZIndex(newLine, 10);
            canvas1.Children.Add(newLine);

        }
        private void canvas1_MouseMove(object sender, MouseEventArgs e)
        {
            Point drawPoint = (Point)e.GetPosition(canvas1);

            if (newLine != null)
            {
                newLine.X2 = drawPoint.X;
                newLine.Y2 = drawPoint.Y;

            }
        }
        private void canvas1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            newLine = null;
        }
    }
    
}

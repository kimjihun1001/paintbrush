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

namespace paintbrush
{ 
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int valueOfZIndex = 1;

        // 현재 그리기 모드를 저장
        public static string presentMode = null;
        public static List<Button> button_mode_List = new List<Button>();

        // 현재 색 선택 모드를 저장
        public static string selectingColor = null;
        public static List<Button> button_selectingColor_List = new List<Button>();

        Point startPoint;

        // 연필로 자유 선 그리기
        public static PathFigure myPathFigure = null;
        public static PathGeometry myPathGeometry = null;
        public static Path myPath = null;

        Line line = null;
        Rectangle rectangle = null;
        Ellipse ellipse = null;

        public MainWindow()
        {
            InitializeComponent();

            // 그리기모드 초기설정
            button_mode_List.Add(btn_pencil);
            button_mode_List.Add(btn_eraser);
            button_mode_List.Add(btn_line);
            button_mode_List.Add(btn_square);
            button_mode_List.Add(btn_paint);
            button_mode_List.Add(btn_circle);
            button_mode_List.Add(btn_select);
            button_mode_List.Add(btn_pipette);

            // 색 선택 모드 초기설정
            button_selectingColor_List.Add(btn_strokeColor);
            button_selectingColor_List.Add(btn_fillColor);

            selectingColor = btn_strokeColor.Name;
        }


        private void btn_selectingColor_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            selectingColor = button.Name;
            foreach (Button button_selectingColor in button_selectingColor_List)
            {
                if (button_selectingColor.Name == selectingColor)
                {
                    button_selectingColor.BorderThickness = new Thickness(5);
                    button_selectingColor.BorderBrush = Brushes.DarkGray;
                }
                else
                {
                    button_selectingColor.BorderThickness = new Thickness(0);

                }
            }
        }

        private void btn_mode_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            presentMode = button.Name;
            foreach (Button button_mode in button_mode_List)
            {
                if (button_mode.Name == presentMode)
                {
                    button_mode.Background = Brushes.White;

                }
                else
                {
                    button_mode.Background = Brushes.DarkGray;
                }
            }

        }

        private void btn_color_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (selectingColor == btn_fillColor.Name)
            {
                btn_fillColor.Background = button.Background;
            }
            else
            {
                btn_strokeColor.Background = button.Background;
            }
        }

        private void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = (Point)e.GetPosition(canvas1);

            switch (presentMode)
            {
                case "btn_pencil":
                    myPathFigure = new PathFigure();
                    myPathFigure.StartPoint = clickPoint;

                    myPathGeometry = new PathGeometry();
                    myPathGeometry.Figures.Add(myPathFigure);

                    myPath = new Path();
                    myPath.Data = myPathGeometry;

                    myPath.Stroke = btn_strokeColor.Background;
                    myPath.StrokeThickness = slider_thickness.Value;
                    // 이거 넣으면 경로들 안쪽이 채워지면서 이상해짐!!
                    // myPath.Fill = btn_strokeColor.Background;

                    Canvas.SetZIndex(myPath, valueOfZIndex);
                    valueOfZIndex++;

                    canvas1.Children.Add(myPath);

                    break;
                case "btn_eraser":
                    myPathFigure = new PathFigure();
                    myPathFigure.StartPoint = clickPoint;

                    myPathGeometry = new PathGeometry();
                    myPathGeometry.Figures.Add(myPathFigure);

                    myPath = new Path();
                    myPath.Data = myPathGeometry;

                    myPath.Stroke = Brushes.White;
                    myPath.StrokeThickness = slider_thickness.Value * 5;

                    Canvas.SetZIndex(myPath, valueOfZIndex);
                    valueOfZIndex++;

                    canvas1.Children.Add(myPath);
                    break;
                case "btn_line":
                    line = new Line();
                    line.X1 = clickPoint.X;
                    line.Y1 = clickPoint.Y;

                    line.Fill = btn_strokeColor.Background;
                    line.Stroke = btn_strokeColor.Background;
                    line.StrokeThickness = slider_thickness.Value;

                    Canvas.SetZIndex(line, valueOfZIndex);
                    valueOfZIndex++;

                    canvas1.Children.Add(line);
                    break;
                case "btn_square":
                    rectangle = new Rectangle();

                    startPoint = new Point();
                    startPoint.X = clickPoint.X;
                    startPoint.Y = clickPoint.Y;

                    Canvas.SetLeft(rectangle, clickPoint.X);
                    Canvas.SetTop(rectangle, clickPoint.Y);

                    rectangle.Fill = btn_fillColor.Background;
                    rectangle.Stroke = btn_strokeColor.Background;
                    rectangle.StrokeThickness = slider_thickness.Value;

                    Canvas.SetZIndex(rectangle, valueOfZIndex);
                    valueOfZIndex++;

                    canvas1.Children.Add(rectangle);
                    break;
                case "btn_paint":


                    break;
                case "btn_circle":
                    ellipse = new Ellipse();

                    startPoint = new Point();
                    startPoint.X = clickPoint.X;
                    startPoint.Y = clickPoint.Y;

                    Canvas.SetLeft(ellipse, clickPoint.X);
                    Canvas.SetTop(ellipse, clickPoint.Y);

                    ellipse.Fill = btn_fillColor.Background;
                    ellipse.Stroke = btn_strokeColor.Background;
                    ellipse.StrokeThickness = slider_thickness.Value;

                    Canvas.SetZIndex(ellipse, valueOfZIndex);
                    valueOfZIndex++;

                    canvas1.Children.Add(ellipse);
                    break;
                case "btn_select":
                    break;
                case "btn_pipette":
                    break;
                default:
                    break;
            }

        }

        // MouseEventArgs로 받지 않고 MouseButtonEventArgs로 받으면 오류 발생함
        private void canvas1_MouseMove(object sender, MouseEventArgs e)
        {
            Point drawPoint = (Point)e.GetPosition(canvas1);

            switch (presentMode)
            {
                case "btn_pencil":
                    if (myPath != null)
                    {
                        LineSegment myLineSegment = new LineSegment();
                        myLineSegment.Point = drawPoint;
                        myPathFigure.Segments.Add(myLineSegment);
                    }
                    break;
                case "btn_eraser":
                    if (myPath != null)
                    {
                        LineSegment myLineSegment = new LineSegment();
                        myLineSegment.Point = drawPoint;
                        myPathFigure.Segments.Add(myLineSegment);
                    }
                    break;
                case "btn_line":
                    if (line != null)
                    {
                        line.X2 = drawPoint.X;
                        line.Y2 = drawPoint.Y;
                    }
                    break;
                case "btn_square":
                    if (rectangle != null)
                    {
                        rectangle.Height = Math.Abs(drawPoint.Y - startPoint.Y);
                        rectangle.Width = Math.Abs(drawPoint.X - startPoint.X);

                        if (drawPoint.X < startPoint.X)
                        {
                            if (drawPoint.Y < startPoint.Y)
                            {
                                Canvas.SetLeft(rectangle, drawPoint.X);
                                Canvas.SetTop(rectangle, drawPoint.Y);

                            }
                            else
                            {
                                Canvas.SetLeft(rectangle, drawPoint.X);

                            }
                        }
                        else
                        {
                            if (drawPoint.Y < startPoint.Y)
                            {
                                Canvas.SetTop(rectangle, drawPoint.Y);

                            }
                            else
                            {
                            }
                        }
                    }
                    break;
                case "btn_paint":
                    break;
                case "btn_circle":
                    if (ellipse != null)
                    {
                        ellipse.Height = Math.Abs(drawPoint.Y - startPoint.Y);
                        ellipse.Width = Math.Abs(drawPoint.X - startPoint.X);

                        if (drawPoint.X < startPoint.X)
                        {
                            if (drawPoint.Y < startPoint.Y)
                            {
                                Canvas.SetLeft(ellipse, drawPoint.X);
                                Canvas.SetTop(ellipse, drawPoint.Y);

                            }
                            else
                            {
                                Canvas.SetLeft(ellipse, drawPoint.X);

                            }
                        }
                        else
                        {
                            if (drawPoint.Y < startPoint.Y)
                            {
                                Canvas.SetTop(ellipse, drawPoint.Y);

                            }
                            else
                            {
                            }
                        }
                    }
                    break;
                case "btn_select":
                    break;
                case "btn_pipette":
                    break;
                default:
                    break;
            }
        }

        private void canvas1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (presentMode)
            {
                case "btn_pencil":
                    myPath = null;
                    myPathFigure = null;
                    myPathGeometry = null;
                    break;
                case "btn_eraser":
                    myPath = null;
                    myPathFigure = null;
                    myPathGeometry = null;
                    break;
                case "btn_line":
                    line = null;
                    break;
                case "btn_square":
                    rectangle = null;
                    break;
                case "btn_paint":
                    break;
                case "btn_circle":
                    ellipse = null;
                    break;
                case "btn_select":
                    break;
                case "btn_pipette":
                    break;
                default:
                    break;
            }
        }

        
    }
}

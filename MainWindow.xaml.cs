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
        public static string PresentMode = null;
        public static List<Button> button_mode_List = new List<Button>();

        Line line = null;
        public MainWindow()
        {
            InitializeComponent();

            button_mode_List.Add(btn_pencil);
            button_mode_List.Add(btn_eraser);
            button_mode_List.Add(btn_line);
            button_mode_List.Add(btn_square);
            button_mode_List.Add(btn_triangle);
            button_mode_List.Add(btn_circle);
            button_mode_List.Add(btn_select);
            button_mode_List.Add(btn_pipette);

        }


        private void btn_mode_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            PresentMode = button.Name;
            foreach (Button button_mode in button_mode_List)
            {
                if (button_mode.Name == PresentMode)
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

            box_presentColor.Background = button.Background;
        }
    }
}

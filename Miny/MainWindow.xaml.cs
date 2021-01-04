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

namespace Miny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int width = 5, height = 5, mines = 5;
        MainGame dalsi;
        public MainWindow()
        {
            InitializeComponent();


        }

        private void Dalsi_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (width * height >= mines)
            {

                dalsi = new MainGame();
                dalsi.Closing += Dalsi_Closing;
                dalsi.Show();
                IsEnabled = false;
            }
            else
            {
                slider3.Value = width * height;
            }
        }


        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (label1 == null) return;
            width = Convert.ToInt32(slider1.Value);
            label1.Content = width;
        }

        private void Slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (label2 == null) return;
            height = Convert.ToInt32(slider2.Value);
            label2.Content = height;
        }

        private void Slider3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (label3 == null) return;
            mines = Convert.ToInt32(slider3.Value);
            label3.Content = mines;
        }
    }
}

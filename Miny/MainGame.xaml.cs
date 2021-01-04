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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Miny
{
    /// <summary>
    /// Interaction logic for MainGame.xaml
    /// </summary>
    public partial class MainGame : Window
    {
        int vyska, sirka;
        int[,] pole;
        int pocetMin;
        int najito, naklikl = 0;
        double fontsize;
        DispatcherTimer casovac = new DispatcherTimer();

        Win someWindow;
        Over oknoKonec;

        public MainGame()
        {
            InitializeComponent();

            for (int i = 0; i < MainWindow.width; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                Mrizka.ColumnDefinitions.Add(colDef);
            }

            for (int i = 0; i < MainWindow.height; i++)
            {
                RowDefinition colDef = new RowDefinition();
                Mrizka.RowDefinitions.Add(colDef);
            }

            vyska = Mrizka.ColumnDefinitions.Count;
            sirka = Mrizka.RowDefinitions.Count;
            pole = new int[sirka, vyska];
            pocetMin = MainWindow.mines;
            VygenerujPole();
            casovac.Interval = new TimeSpan(0, 0, 2);
            casovac.Tick += Casovac_Tick;


            for (int i = 0; i < vyska; i++)
            {
                for (int j = 0; j < sirka; j++)
                {
                    Rectangle rec = new Rectangle();
                    rec.Fill = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    rec.MouseLeftButtonDown += Rec_MouseDown;
                    rec.MouseRightButtonDown += Rec_MouseRightButtonDown;
                    rec.SetValue(Grid.RowProperty, j);
                    rec.SetValue(Grid.ColumnProperty, i);
                    Mrizka.Children.Add(rec);

                    Border ohraniceni = new Border();
                    ohraniceni.BorderBrush = Brushes.Black;
                    ohraniceni.BorderThickness = new Thickness(1);
                    ohraniceni.SetValue(Grid.RowProperty, j);
                    ohraniceni.SetValue(Grid.ColumnProperty, i);
                    Mrizka.Children.Add(ohraniceni);
                }
            }


        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            fontsize = ActualHeight / vyska * 0.5;
        }

        private void Konec()
        {
            IsEnabled = false;
            casovac.Start();

        }

        private void Casovac_Tick(object sender, EventArgs e)
        {

            Close();
            try
            {
                someWindow.Close();

            }
            catch
            {

            }
            try
            {
                oknoKonec.Close();
            }
            catch
            {

            }
        }

        private void Rec_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var recSel = ((Rectangle)sender);
            int x = Convert.ToInt32(recSel.GetValue(Grid.RowProperty));
            int y = Convert.ToInt32(recSel.GetValue(Grid.ColumnProperty));

            if (Convert.ToString(recSel.Fill) == "#FFFF00FF")
            {
                naklikl--;
            }

            if (pole[x, y] == 1)
            {
                Projed();
                oknoKonec = new Over();
                Konec();
                oknoKonec.ShowDialog();
            }
            else
            {
                int soucet = 0;

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i != 0 || j != 0)
                        {
                            int mezi;

                            if (x + i != -1 && y + j != -1 && x + i != sirka && y + j != vyska)
                            {
                                mezi = pole[x + i, y + j];
                            }
                            else mezi = 0;


                            soucet += mezi;
                        }
                    }
                }

                if (soucet >= 1)
                {
                    recSel.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                    Label cislo = new Label();
                    cislo.FontSize = fontsize;
                    cislo.Content = soucet;
                    cislo.SetValue(Grid.RowProperty, x);
                    cislo.SetValue(Grid.ColumnProperty, y);
                    Mrizka.Children.Add(cislo);
                }
                else
                {
                    recSel.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                }
            }
            Nadpis(naklikl, pocetMin);
        }

        private void Rec_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var recSel = ((Rectangle)sender);
            int x = Convert.ToInt32(recSel.GetValue(Grid.RowProperty));
            int y = Convert.ToInt32(recSel.GetValue(Grid.ColumnProperty));

            if (pole[x, y] == 1 && Convert.ToString(recSel.Fill) == "#FF646464")
            {
                najito++;
                naklikl++;
                recSel.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 255));
            }
            else if (Convert.ToString(recSel.Fill) == "#FFFF00FF")
            {
            }
            else
            {
                naklikl++;
                recSel.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 255));
            }

            if (naklikl == pocetMin && pocetMin == najito)
            {
                someWindow = new Win();
                Konec();
                someWindow.ShowDialog();
            }
            Nadpis(naklikl, pocetMin);
        }

        private void VygenerujPole()
        {
            Random rnd = new Random();
            for (int i = 0; i < pocetMin; i++)
            {
                int randomX = rnd.Next(0, sirka);
                int randomY = rnd.Next(0, vyska);
                if (pole[randomX, randomY] == 0)
                {
                    pole[randomX, randomY] = 1;
                }
                else
                {
                    i--;
                }
            }
        }

        private void Projed()
        {
            for (int i = 0; i < pole.GetLength(0); i++)
            {
                for (int j = 0; j < pole.GetLength(1); j++)
                {
                    if (pole[i, j] == 1)
                    {
                        Rectangle rec = new Rectangle();
                        rec.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                        rec.SetValue(Grid.RowProperty, i);
                        rec.SetValue(Grid.ColumnProperty, j);
                        Mrizka.Children.Add(rec);
                    }
                }
            }
        }

        private void Nadpis(int ma,int celkem)
        {
            window.Title = $"Najito min {ma}/{celkem}";
        }
    }
}

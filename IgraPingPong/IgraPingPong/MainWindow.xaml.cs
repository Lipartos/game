﻿using System;
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
using System.Windows.Threading;

namespace IgraPingPong
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int Time;

        Ellipse O;

        double R = 10;

        double x, y;

        double V = 3;
        double vx, vy;


        Rectangle Plate;

        double H = 100;
        double Px;
        double Pv = 25;

        DispatcherTimer Timer;

        public MainWindow()
        {
            InitializeComponent();

            Restart();

            O = new Ellipse();
            O.Fill = Brushes.Red;
            O.Width = 2 * R;
            O.Height = 2 * R;
            O.Margin = new Thickness(x, y, 0, 0);
            g.Children.Add(O);


            Plate = new Rectangle();
            Plate.Fill = Brushes.Blue;
            Plate.Width = H;
            Plate.Height = 5;
            Px = g.Width / 2 - H / 2;
            Plate.Margin = new Thickness(Px, g.Height, 0, 0);
            g.Children.Add(Plate);

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(onTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            Timer.Start();
        }

        void Restart()
        {
            x = g.Width / 2 - R;
            y = g.Height / 2 - R;

            Random rnd = new Random();

            double alpha = rnd.NextDouble() * Math.PI / 2 + Math.PI / 4;

            vx = V * Math.Cos(alpha);
            vy = V * Math.Sin(alpha);

            Px = g.Width / 2 - H / 2;

            Time = 0;
        }

        void onTick(object sender, EventArgs e)
        {
            Time++;

            if ((x < 0) || (x > g.Width - 2 * R))
            {
                vx = -vx;
            }

            if ((y < 0) || (y > g.Height - 2 * R))
            {
                vy = -vy;
            }

            if (y > g.Height - 2 * R)
            {
                double c = x + R;


                if ((c >= Px) && (c <= Px + H))
                {
                    vx *= 1.1;
                    vy *= 1.1;
                }
                else
                {
                    MessageBox.Show("Game Over");
                    Restart();
                    Plate.Margin = new Thickness(Px, g.Height, 0, 0);
                }
            }

            x += vx;
            y += vy;

            O.Margin = new Thickness(x, y, 0, 0);

            tbTime.Text = (Time / 100).ToString();
        }


        private void cmKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                Px -= Pv;
            }

            if (e.Key == Key.Right)
            {
                Px += Pv;
            }

            if (Px < 0)
            {
                Px = 0;
            }

            if (Px > g.Width - H)
            {
                Px = g.Width - H;
            }

            Plate.Margin = new Thickness(Px, g.Height, 0, 0);

        }
    }
}

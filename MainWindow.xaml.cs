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

namespace ChClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1.0);
            timer.Start();
            timer.Tick += new EventHandler(delegate (object s, EventArgs a)
            {

                time.Text = "" + DateTime.Now.ToString("HH") + ":"
              + DateTime.Now.ToString("mm");

                day.Text = "" + DateTime.Now.DayOfWeek;

                date.Text = "" + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Day;
            });
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
            this.Activate();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }

    }
}
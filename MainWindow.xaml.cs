using NHotkey;
using NHotkey.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1.0)
            };
            timer.Start();
            timer.Tick += new EventHandler(delegate(object s, EventArgs a)
            {
                time.Text = DateTime.Now.ToString("HH") + ":"
                                                        + DateTime.Now.ToString("mm");

                day.Text = "" + DateTime.Now.DayOfWeek;

                date.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Day;
            });

            //Implementing hotkeys
            HotkeyManager.Current.AddOrReplace("Show or hide", Key.C, ModifierKeys.Control | ModifierKeys.Shift,
                OnHotKey);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Topmost = true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            var window = (Window) sender;
            window.Topmost = true;
            Activate();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Window_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var window = (Window) sender;
            window.Topmost = true;
        }

        private void OnHotKey(object sender, HotkeyEventArgs e)
        {
            TrayIconClick(sender, null);
        }

        private void TrayIconClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.IsVisible)
                Application.Current.MainWindow.Hide();
            else
                Application.Current.MainWindow.Show();
        }

        //Settings menu item
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = Application.Current.Windows.OfType<SettingsWindow>().FirstOrDefault();
            if (settingsWindow == null)
            {
                settingsWindow = new SettingsWindow();
                settingsWindow.Show();
            }
            else
            {
                settingsWindow.Focus();
            }
        }

        //Single instance management for app windows
        public static void OpenWindow<T>() where T : Window
        {
            var windows = Application.Current.Windows.Cast<Window>();
            var any = windows.Any(s => s is T);
            if (any)
            {
                var subWindow = windows.Where(s => s is T).ToList()[0];
                if (subWindow.WindowState == WindowState.Minimized)
                    subWindow.WindowState = WindowState.Normal;
                subWindow.Focus();
            }
            else
            {
                var subWindow = (Window) Activator.CreateInstance(typeof(T));
                subWindow.Show();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
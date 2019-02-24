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
using System.Windows.Forms;
using System.Drawing;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows.Threading;
using Tulpep.NotificationWindow;
using System.Diagnostics;
using ContextMenu = System.Windows.Controls.ContextMenu;
using MessageBox = System.Windows.MessageBox;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;
        bool stat = false;
       
        public MainWindow()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                clocktxtblock.Text = currentTime;
            }
        }

        private void startbtn_Click(object sender, RoutedEventArgs e)
        {
            sw.Start();
            dt.Start();
            stat = true;
            status.Text = stat.ToString();


            //System.Windows.Forms.NotifyIcon notifyIconn = new System.Windows.Forms.NotifyIcon();
            //notifyIconn.Icon = new System.Drawing.Icon("Bulb.ico");
            //notifyIconn.Visible = true;
            //notifyIconn.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_Click);
            //notifyIconn.ShowBalloonTip(12000, "aaaa", "bbbb", System.Windows.Forms.ToolTipIcon.Info);
            //notifyIconn.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_DClick);
            //System.Windows.Forms.ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
            Tulpep.NotificationWindow.PopupNotifier popup = new Tulpep.NotificationWindow.PopupNotifier();
            popup.TitleText = "motfk";
            popup.ContentText = "wff";
            System.Windows.Forms.CheckBox cb = new System.Windows.Forms.CheckBox();
           
            popup.Popup();

            
            //contextMenu.MenuItems.Add("closeded", new EventHandler(Close));

            //notifyIcon.ContextMenu = notifyIconContextMenu;
        }

        //private void notifyIcon_DClick(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    if (e.Button == System.Windows.Forms.MouseButtons.Left)
        //    {
        //        MessageBox.Show("dobuel clsick");
        //    }
        //}

        private void Close(object sender, EventArgs e)
        {
            MessageBox.Show("haha");
        }

        private void notifyIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MessageBox.Show("notify icon cleicke");
            }
        }

        private void stopbtn_Click(object sender, RoutedEventArgs e)
        {
            if (sw.IsRunning)
            {
                stat = false;
                status.Text = stat.ToString();
                sw.Stop();
            }
            elapsedtimeitem.Items.Add(currentTime);
        }

        private void resetbtn_Click(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            stat = false;
            status.Text = stat.ToString();
            clocktxtblock.Text = "00:00:00";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            status.Text = stat.ToString();

          
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Tulpep.NotificationWindow;

namespace WpfApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

   
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    NotifyIcon notifyIcon = new NotifyIcon();

        //    notifyIcon1.BalloonTipTitle = string.Format(Common.Properties.Resources.ApplicationMinimizeHelpText, _window.Title);

        //    notifyIcon1.MouseClick += notifyIcon_MouseClick;

        //    notifyIcon1.MouseDoubleClick += notifyIcon_MouseDoubleClick;

        //    notifyIcon1.BalloonTipClicked += notifyIcon_BalloonTipClicked;
        //    var _trayMenu = new ContextMenu();

        //    _trayMenu.MenuItems.Add("Open", (sender, args) => HandleSystemTrayVisibility(false));

        //    _trayMenu.MenuItems.Add("Exit", (sender, args) =>

        //    {

        //        _exitRequested = true;

        //        Application.Current.Shutdown();

        //    });

        //    _notifyIcon.ContextMenu = _trayMenu;
        //}
    }
}

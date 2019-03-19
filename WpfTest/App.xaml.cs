using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //store api server
        public string api_server { get; set; }
        //store api mockup server
        public string api_mockup_server { get; set; }
        //store apikey
        public string api_key { get; set; }
        //store project id and name
        public string project_id { get; set; }
        public string project_name { get; set; }

        //store version id and name
        public string version_id { get; set; }
        public string version_name { get; set; }
        //store work package id and name
        public string workpackage_id { get; set; }
        public string workpackage_name { get; set; }
   
        //store activity id and name
        public string activity_id { get; set; }
        public string activity_name { get; set; }
        // store user id
        public string u_id { get; set; }
        //define Taskbar icon for app resource
        private TaskbarIcon tb;

        public class TE_Settingg
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public List<TE_Settingg> elements { get; set; }





        private void InitApplication()
        {
            //initialize NotifyIcon
            tb = (TaskbarIcon)FindResource("MyNotifyIcon");

        }
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
        //    tb = (TaskbarIcon)FindResource("NotifyIcon");
        //}

        //protected override void OnExit(ExitEventArgs e)
        //{
        //    tb.Dispose(); //the icon would clean up automatically, but this is cleaner
        //    base.OnExit(e);
        //}
    }
    
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using MahApps.Metro.Controls;
using System.Text.RegularExpressions;
using DataFormat = RestSharp.DataFormat;
using System.Net;
using static WpfTest.LogTimeManual_Window;
using WpfTest.Class.Log;
using System.Windows.Controls.Primitives;
using Hardcodet.Wpf.TaskbarNotification;
using System.IO;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for LogTimeAuto_Window.xaml
    /// </summary>
    public partial class LogTimeAuto_Window : MetroWindow
    {
        //validate
        private int _noOfErrorsOnScreen = 0;
        private Log_Hour _logtime = new Log_Hour();
        //timer track for log time
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;
    //    bool stat = false;
        //countdown timer for popup
        DispatcherTimer _timer;
        TimeSpan _time;

    
        public LogTimeAuto_Window()
        {
            InitializeComponent();
          
            grid.DataContext = _logtime;
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 1);

        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _noOfErrorsOnScreen++;
            else
                _noOfErrorsOnScreen--;
        }

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _noOfErrorsOnScreen == 0;
            e.Handled = true;
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Log_Hour cust = grid.DataContext as Log_Hour;
            // reset UI
            _logtime = new Log_Hour();
            grid.DataContext = _logtime;
            e.Handled = true;

        }
        void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",ts.Hours, ts.Minutes, ts.Seconds);
                clocktxtblock.Text = currentTime;
            }
        }
        private void startbtn_Click(object sender, RoutedEventArgs e)
        {
            sw.Start();
            dt.Start();

            ///for countdown timer to popup notifications
            countdown_Start();
        }
        public void countdown_Start()
        {
            _time = TimeSpan.FromSeconds(5);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    ShowStandardBalloon();
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }
       
        public void stopbtn_Click(object sender, RoutedEventArgs e)
        {
          
            if (sw.IsRunning)
            {
                sw.Stop();
                //format HH:MM:SS to decimal   
                decimal dec = Convert.ToDecimal(TimeSpan.Parse(currentTime).TotalHours);
                //roundup to 2 decimal place
                dec = Math.Round(dec, 2);

                tb_LogHour.Text = dec.ToString(new CultureInfo("en-US"));
            }
            _timer.Stop();
        }
        private void resetbtn_Click(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            clocktxtblock.Text = "00:00:00";
        }
        private void ShowStandardBalloon()
        {

            //TaskbarIcon tb = new TaskbarIcon();
            //tb = (TaskbarIcon)FindResource("NotifyIcon");
            FancyBalloon balloon = new FancyBalloon();
            MyNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Fade, 15000);

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //TaskbarIcon tb = new TaskbarIcon();
          

            string project_id = (App.Current as App).project_id;
            string workpackage_id = (App.Current as App).workpackage_id;
            string project_name = (App.Current as App).project_name;
            string workpackage_name = (App.Current as App).workpackage_name;
            Project.Text = project_name;
            WorkPackage.Text = workpackage_name;

            string file_path = @"D:\New folder\WpfTest\WpfTest\TE_Activities.txt";
            List<TE_Setting> setting = new List<TE_Setting>();
            List<string> lines = File.ReadAllLines(file_path).ToList();
            foreach (var line in lines)
            {
                string[] entries = line.Split(',');
                TE_Setting new_setting = new TE_Setting();
                new_setting.id = entries[0];
                new_setting.name = entries[1];
                setting.Add(new_setting);
            }


            //Add activity type to combo box
            List<ComboBoxActivity> cbA = new List<ComboBoxActivity>();
            var tea_id = 0;
            while (tea_id < lines.Count)
            {
                cbA.Add(new ComboBoxActivity(setting[tea_id].id, setting[tea_id].name));
                tea_id++;
            }
            //bind to the Activity combobox in xaml
            Activity.DisplayMemberPath = "value";
            Activity.SelectedValuePath = "key";
            Activity.ItemsSource = cbA;
        }

        private void LogTime_Click(object sender, RoutedEventArgs e)
        {
            string project_id = (App.Current as App).project_id;
            string workpackage_id = (App.Current as App).workpackage_id;
            // get access to cbox key
            ComboBoxActivity cbA = (ComboBoxActivity)Activity.SelectedItem;
            string activity_type = cbA.key;

            string log_hour = tb_LogHour.Text;
            string comment = tb_Comment.Text;
            DateTime date = (DateTime)datePicker.SelectedDate;
            string result = date.ToString("yyyy-MM-dd");
            string user_id = (App.Current as App).u_id;
  
            RootObject time_entry = new RootObject()
            {
                _links = new Links
                {
                    project = new LinksProperty
                    {
                        href = "/api/v3/projects/" + project_id
                    },
                    activity = new LinksProperty
                    {
                        href = "/api/v3/time_entries/activities/" + activity_type
                    },
                    workPackage = new LinksProperty
                    {
                        href = "/api/v3/work_packages/" + workpackage_id
                    },
                    customField4 = new LinksProperty
                    {
                        href = "/api/v3/users/" + user_id
                    }
                },
                hours = "PT" + log_hour + "H",
                comment = comment,
                spentOn = result
            };

            var json = JsonConvert.SerializeObject(time_entry);
            string api_server = (App.Current as App).api_server;
            var client = new RestClient(api_server);
            var request = new RestRequest("/time_entries", Method.POST);
            string password = (App.Current as App).api_key;
            client.Authenticator = new HttpBasicAuthenticator("apikey", password);

            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(json);
            IRestResponse response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            int numbericStatusCode = (int)statusCode;
            if (numbericStatusCode == 200 || numbericStatusCode == 201 || numbericStatusCode == 301)
            {
                MessageBox.Show("Log time success!");
            }
            else
            {
                MessageBox.Show("Log time failed!");
            }

        }
    }
}

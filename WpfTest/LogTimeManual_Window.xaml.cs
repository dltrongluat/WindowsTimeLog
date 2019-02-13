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
using System.Windows.Navigation;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.RegularExpressions;
using DataFormat = RestSharp.DataFormat;
using System.Net;
using MahApps.Metro.Controls;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for LogTimeManual_Window.xaml
    /// </summary>
    public partial class LogTimeManual_Window : MetroWindow
    {
        private int _noOfErrorsOnScreen = 0;
        private LogTime _logtime = new LogTime();
        public class LinksProperty
        {
            public string href { get; set; }
        }
        // create a class of combobox activity type
        public class ComboBoxActivity
        {
            public string key { get; set; }
            public string value { get; set; }
            public ComboBoxActivity(string _key, string _value)
            {
                key = _key;
                value = _value;
            }

        }
        // class for time entries activities object
        public class TE_Activity
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class RootObjectActivity
        {
            public List<TE_Activity> activities { get; set; }
        }
        // class for log time object
        public class Links
        {
            public LinksProperty project { get; set; }
            public LinksProperty activity { get; set; }
            public LinksProperty workPackage { get; set; }
            public LinksProperty customField4 { get; set; }
        }
        
        public class RootObject
        {
            public Links _links { get; set; }
            public string hours { get; set; }
            public string comment { get; set; }
            public string spentOn { get; set; }
        }
        public LogTimeManual_Window()
        {
            InitializeComponent();
            grid.DataContext = _logtime;
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
            LogTime cust = grid.DataContext as LogTime;


            // reset UI
            _logtime = new LogTime();
            grid.DataContext = _logtime;
            e.Handled = true;

        }

       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string project_id = (App.Current as App).project_id;
            string workpackage_id = (App.Current as App).workpackage_id;
            string project_name = (App.Current as App).project_name;
            string workpackage_name = (App.Current as App).workpackage_name;
            Project.Text = project_name;
            WorkPackage.Text = workpackage_name;

            //Add activity type to combo box
            List<ComboBoxActivity> cbA = new List<ComboBoxActivity>();
            var client = new RestClient("http://localhost:3000/api/time_entries/");
            var request = new RestSharp.RestRequest("activities", Method.GET);        
            IRestResponse response = client.Execute(request);
            RootObjectActivity te_activity = JsonConvert.DeserializeObject<RootObjectActivity>(response.Content);
            var tea_id = 0;
            while (tea_id < te_activity.activities.Count)
            {
                cbA.Add(new ComboBoxActivity(te_activity.activities[tea_id].id, te_activity.activities[tea_id].name));
                tea_id++;
            }
            //bind to the Activity combobox in xaml
            Activity.DisplayMemberPath = "value";
            Activity.SelectedValuePath = "key";
            Activity.ItemsSource = cbA;

        }

        private void Post_Click_1(object sender, RoutedEventArgs e)
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

            MessageBox.Show(user_id);
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

            var client = new RestClient("https://luattest2.openproject.com/api/v3/");
            var request = new RestRequest("time_entries", Method.POST);
            var password = ((Login)Application.Current.MainWindow).API_Key.Text;
            client.Authenticator = new HttpBasicAuthenticator("apikey", password);

            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(json);
            IRestResponse response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;


            MessageBox.Show(statusCode.ToString());

        }



    }

  
}

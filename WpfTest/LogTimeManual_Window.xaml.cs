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
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.RegularExpressions;
using DataFormat = RestSharp.DataFormat;
using System.Net;
namespace WpfTest
{
    /// <summary>
    /// Interaction logic for LogTimeManual_Window.xaml
    /// </summary>
    public partial class LogTimeManual_Window : Window
    {
        public LogTimeManual_Window()
        {
            InitializeComponent();
        }
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
            cbA.Add(new ComboBoxActivity("1", "Management"));
            cbA.Add(new ComboBoxActivity("2", "Specification"));
            cbA.Add(new ComboBoxActivity("3", "Development"));
            cbA.Add(new ComboBoxActivity("4", "Testing"));
            cbA.Add(new ComboBoxActivity("5", "Support"));
            cbA.Add(new ComboBoxActivity("6", "Other"));
            //bind to the Activity combobox in xaml
            Activity.DisplayMemberPath = "value";
            Activity.SelectedValuePath = "key";
            Activity.ItemsSource = cbA;

        }

    
       
        private void Post_Click_1(object sender, RoutedEventArgs e)
        {
            string project_id = (App.Current as App).project_id;
            string workpackage_id = (App.Current as App).workpackage_id;
            //get access to cbox key
            ComboBoxActivity cbA = (ComboBoxActivity)Activity.SelectedItem;
            string activity_type = cbA.key;
            
            string log_hour = LogHour.Text;
            string comment = Comment.Text;
            DateTime date = (DateTime)datePicker.SelectedDate;
            string result = date.ToString("yyyy-MM-dd");
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
                        href = "/api/v3/users/1"
                    }
                },
                hours = "PT" + log_hour + "H",
                comment = comment,
                spentOn = result
            };

            var json = JsonConvert.SerializeObject(time_entry);

            var client = new RestClient("https://luattest.openproject.com/api/v3/");
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

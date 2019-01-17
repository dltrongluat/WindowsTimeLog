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
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.RegularExpressions;
using DataFormat = RestSharp.DataFormat;
using System.Net;
using Newtonsoft.Json.Linq;
namespace WpfTest
{
    /// <summary>
    /// Interaction logic for LogTimeManual_Page.xaml
    /// </summary>
    public partial class LogTimeManual_Page : Page
    {
        public LogTimeManual_Page()
        {
            InitializeComponent();
        }

      
        public class LinksProperty
        {
            public string href { get; set; }
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

        private void Post_Click_1(object sender, RoutedEventArgs e)
        {

            RootObject time_entry = new RootObject()
            {
                _links = new Links
                {
                    project = new LinksProperty
                    {
                        href = "/api/v3/projects/2"
                    },
                    activity = new LinksProperty
                    {
                        href = "/api/v3/time_entries/activities/2"
                    },
                    workPackage = new LinksProperty
                    {
                        href = "/api/v3/work_packages/24"
                    },
                    customField4 = new LinksProperty
                    {
                        href = "/api/v3/users/1"
                    }
                },
                hours = "PT2H",
                comment = "test ne ne",
                spentOn = "2018-01-01"
            };

            var json = JsonConvert.SerializeObject(time_entry);

            var client = new RestClient("https://luattest.openproject.com/api/v3/");
            var request = new RestRequest("time_entries", Method.POST);

            client.Authenticator = new HttpBasicAuthenticator("apikey", "d72418d210c301b0a8c6275a2c34f6df51f300947d9f1206baa9464afb683454");


            request.AddHeader("Content-Type", "application/json");


            request.AddJsonBody(json);

            //textBox.Text = json;

            IRestResponse response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;


            MessageBox.Show(statusCode.ToString());

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string project_id = (App.Current as App).project_id;
            string workpackage_id = (App.Current as App).workpackage_id;
            string project_name = (App.Current as App).project_name;
            string workpackage_name = (App.Current as App).workpackage_name;
            Project.Text = project_name;
            WorkPackage.Text = workpackage_name;
        }
    }

  

}

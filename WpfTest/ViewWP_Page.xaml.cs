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
using System.Collections.ObjectModel;
using System.Xml;
using System.ComponentModel;
namespace WpfTest
{
   
    public partial class ViewWP_Page : Page
    {
        public ViewWP_Page()
        {
            InitializeComponent();
        }
        /////////////////////////////////////Start of get///////////////
        public class Outer
        {
            public string _type { get; set; }
            public int total { get; set; }
            public int count { get; set; }

            public EmbeddedWorkPackage _embedded { get; set; }
        }
     
        public class EmbeddedWorkPackage
        {
            public List<WorkPackage> elements { get; set; }
        }
        public class WorkPackage
        {
          
            public string id { get; set; }
            public string subject { get; set; }

            public string spentTime { get; set; }
            public Links _links { get; set; }
            //public WorkPackage(string id, string subject, string spentTime, Links _links)
            //{
            //    this.id = id;
            //    this.subject = subject;
            //    this.spentTime = spentTime;
            //    this._links = _links;
            //}
            public static WorkPackage SubjectWithoutNewline(WorkPackage wp)
            {
                WorkPackage new_wp = new WorkPackage()
                {
                    id = wp.id,
                    subject = Regex.Replace(wp.subject, @"\t|\n|\r", ""),
                    //TimeSpan aaa = XmlConvert.ToTimeSpan(obj._embedded.elements[0].spentTime)
                    spentTime = XmlConvert.ToTimeSpan(wp.spentTime).TotalHours.ToString() + "H",
                    _links = wp._links
                };
                return new_wp;
            }

        }
        public class Links
        {
            public Version version { get; set; }
        }
        public class Version
        {
            public string href { get; set; }
            public string title { get; set; }
        }
        public Uri Source { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://luattest2.openproject.com/");
            var password = ((Login)Application.Current.MainWindow).API_Key.Text;
            client.Authenticator = new HttpBasicAuthenticator("apikey", password);
            string project_id = (App.Current as App).project_id;
            var endpoint= "api/v3/projects/"+ project_id + "/work_packages";
            var request = new RestSharp.RestRequest(endpoint, Method.GET);           
            IRestResponse response = client.Execute(request);
            var obj = JsonConvert.DeserializeObject<Outer>(response.Content);
           
            List<WorkPackage> wp_without_newline = obj._embedded.elements.ConvertAll(
                new Converter<WorkPackage, WorkPackage>(WorkPackage.SubjectWithoutNewline));

            var WP = new ObservableCollection<WorkPackage>();
            foreach (var item in wp_without_newline)
                WP.Add(item);

            wpListView.ItemsSource = WP;
          
        }

        private void LogTimeMan_Click(object sender, RoutedEventArgs e)
        {
            // get project id & name
         
            //set work package id from click action
            dynamic selected_WP = (WorkPackage)wpListView.SelectedItem;
            var workpackage_id = selected_WP.id.ToString();
            var workpackage_name = selected_WP.subject.ToString();
           
            (App.Current as App).workpackage_id = workpackage_id;
            (App.Current as App).workpackage_name = workpackage_name;

            //display a new MainWindow
            LogTimeManual_Window LogTimeMan_window = new LogTimeManual_Window();
            LogTimeMan_window.Show();

            //close Window1
            //Window1 w = Application.Current.Windows.OfType<Window1>().FirstOrDefault();
            //w.Close();
        }

        private void LogTimeAuto_Click(object sender, RoutedEventArgs e)
        {
            dynamic selected_WP = (WorkPackage)wpListView.SelectedItem;
            var workpackage_id = selected_WP.id.ToString();
            var workpackage_name = selected_WP.subject.ToString();
            (App.Current as App).workpackage_id = workpackage_id;
            (App.Current as App).workpackage_name = workpackage_name;

            LogTimeAuto_Window LogTimeAuto_window = new LogTimeAuto_Window();
            LogTimeAuto_window.Show();
        }
    }
}

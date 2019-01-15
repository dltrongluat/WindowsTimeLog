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

namespace WpfTest
{
    /// <summary>
    /// </summary>
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
        public class EmbeddedProject
        {
            public List<Project> elements { get; set; }
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

            public static WorkPackage SubjectWithoutNewline(WorkPackage wp)
            {
                WorkPackage new_wp = new WorkPackage()
                {
                 
                    id = wp.id,
                    subject = Regex.Replace(wp.subject, @"\t|\n|\r", ""),
                    //TimeSpan aaa = XmlConvert.ToTimeSpan(obj._embedded.elements[0].spentTime)
                    spentTime = XmlConvert.ToTimeSpan(wp.spentTime).TotalHours.ToString() +"H"

                };

                return new_wp;
            }
        }
        public Uri Source { get; set; }
        public class Project
        {

            public string id { get; set; }
            //public string _type { get; set; }
            public string name { get; set; }
            public string identifier { get; set; }
            public string createdAt { get; set; }

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://luattest.openproject.com/");

            var password = ((Login)Application.Current.MainWindow).API_Key.Text;
            client.Authenticator = new HttpBasicAuthenticator("apikey", password);
            // var project_id = ((ViewProject_Page)Application.Current.MainWindow).Button_Click.Name;

            string test = (App.Current as App).DeptName;
            var endpoint= "api/v3/projects/"+ test +"/work_packages";
            var request = new RestSharp.RestRequest(endpoint, Method.GET);
                
            IRestResponse response = client.Execute(request);
          

            var obj = JsonConvert.DeserializeObject<Outer>(response.Content);

            List<WorkPackage> wp_without_newline = obj._embedded.elements.ConvertAll(
                new Converter<WorkPackage, WorkPackage>(WorkPackage.SubjectWithoutNewline));
            //ObservableCollection<WorkPackage> WorkPackage = obj._embedded.elements.ConvertAll(new Converter<WorkPackage, WorkPackage>(WorkPackage.SubjectWithoutNewline));
            wpListView.ItemsSource = wp_without_newline;
            
           
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

       



        ///////////////////////////end of get method ///////////////


    }
}

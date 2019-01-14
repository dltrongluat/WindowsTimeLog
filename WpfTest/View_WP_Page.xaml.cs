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

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for View_WP_Page.xaml
    /// </summary>
    public partial class View_WP_Page : Page
    {
        public View_WP_Page()
        {
            InitializeComponent();
        }
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
            public string _type { get; set; }
            public string id { get; set; }
            public string subject { get; set; }

            public string spentTime { get; set; }

            public static WorkPackage SubjectWithoutNewline(WorkPackage wp)
            {
                WorkPackage new_wp = new WorkPackage()
                {
                    _type = wp._type,
                    id = wp.id,
                    subject = Regex.Replace(wp.subject, @"\t|\n|\r", ""),
                    spentTime = Regex.Replace(wp.spentTime, @"PT", "")

                };

                return new_wp;
            }
        }
        public class Project
        {

            public string id { get; set; }
            //public string _type { get; set; }
            public string name { get; set; }
            public string identifier { get; set; }
            public string createdAt { get; set; }

      
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://luattest.openproject.com/api/v3/projects/1/");
            client.Authenticator = new HttpBasicAuthenticator("apikey", "d805ff8636e8661cffe5e4f3a1cddafa326ee6182084e7e0083b669cc3d018f6");

            var request = new RestSharp.RestRequest("work_packages", Method.GET);

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            var obj = JsonConvert.DeserializeObject<Outer>(response.Content);
            List<WorkPackage> wp_without_newline = obj._embedded.elements.ConvertAll(
                new Converter<WorkPackage, WorkPackage>(WorkPackage.SubjectWithoutNewline));

            wpListView.ItemsSource = wp_without_newline;
        //    ABC.Text = obj.ToString();
        }
    }
}

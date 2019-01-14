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
    /// Interaction logic for ViewProject_Page.xaml
    /// </summary>
    public partial class ViewProject_Page : Page
    {
        public ViewProject_Page()
        {
            InitializeComponent();
        }
        public class Outer
        {
            public string _type { get; set; }
            public int total { get; set; }
            public int count { get; set; }

            public EmbeddedProject _embedded { get; set; }

        }
        public class EmbeddedProject
        {
            public List<Project> elements { get; set; }
        }
        public class Project
        {

            public string id { get; set; }
            public string name { get; set; }
            public string identifier { get; set; }
            public string createdAt { get; set; }

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://luattest.openproject.com/");
      
            var password = ((Login)Application.Current.MainWindow).API_Key.Text;
            client.Authenticator = new HttpBasicAuthenticator("apikey", password);

            var request = new RestSharp.RestRequest("api/v3/projects", Method.GET);

            IRestResponse response = client.Execute(request);
          

            var obj = JsonConvert.DeserializeObject<Outer>(response.Content);


            projectListView.ItemsSource = obj._embedded.elements;

        }
    }
}

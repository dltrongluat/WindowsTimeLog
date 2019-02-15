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
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;
using System.Data;
namespace WpfTest
{
    /// <summary>
    /// Interaction logic for ViewVersion_Page.xaml
    /// </summary>
    public partial class ViewVersion_Page : Page
    {
        public ViewVersion_Page()
        {
            InitializeComponent();
        }
        public class Self
        {
            public string href { get; set; }
            public string title { get; set; }
        }

        public class DefiningProject
        {
            public string href { get; set; }
            public string title { get; set; }
        }

        public class Links
        {
            public Self self { get; set; }
            public DefiningProject definingProject { get; set; }
        }

        public class Version
        {
            public int id { get; set; }
            public string name { get; set; }
            public string startDate { get; set; }
            public string endDate { get; set; }
            public string status { get; set; }
            public Links _links { get; set; }
        }

        public class Embedded
        {
            public List<Version> elements { get; set; }
        }

        public class RootObject
        {
            public Embedded _embedded { get; set; }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var client = new RestClient("https://luattest2.openproject.com/");
            var password = ((Login)Application.Current.MainWindow).API_Key.Text;
            client.Authenticator = new HttpBasicAuthenticator("apikey", password);
            var request = new RestSharp.RestRequest("api/v3/projects/2/versions", Method.GET);
            IRestResponse response = client.Execute(request);
            var obj = JsonConvert.DeserializeObject<RootObject>(response.Content);
            ObservableCollection<Version> Version = new ObservableCollection<Version>(obj._embedded.elements);
          
            versionListView.ItemsSource = Version;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

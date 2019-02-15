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
            var client = new RestClient("https://luattest2.openproject.com/");
            var password = ((Login)Application.Current.MainWindow).API_Key.Text;
            client.Authenticator = new HttpBasicAuthenticator("apikey", password);
            var request = new RestSharp.RestRequest("api/v3/projects", Method.GET);
            IRestResponse response = client.Execute(request);
            var obj = JsonConvert.DeserializeObject<Outer>(response.Content);
            ObservableCollection<Project> Project = new ObservableCollection<Project>(obj._embedded.elements);
          
            projectListView.ItemsSource = Project;
      
        }

        //private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        //{
        //    Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
        //    e.Handled = true;
        //    dynamic selectedItem = (Project)projectListView.SelectedItem; 
        //    var name = selectedItem.name.ToString();

        //    MessageBox.Show(name);

        //}
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //set project id from click action

            dynamic selected_Project = (Project)projectListView.SelectedItem;
            var project_id = selected_Project.id.ToString();
            var project_name = selected_Project.name.ToString();
            (App.Current as App).project_name = project_name;
            (App.Current as App).project_id = project_id;

            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("ViewWP_Page.xaml", UriKind.RelativeOrAbsolute));

        }
    }
}


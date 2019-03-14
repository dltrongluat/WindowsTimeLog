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
using WpfTest.Class;
using MahApps.Metro.IconPacks;

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
      
      
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
          
            string api_server = (App.Current as App).api_server;
            var client = new RestClient(api_server);
            string password = (App.Current as App).api_key;
       
            client.Authenticator = new HttpBasicAuthenticator("apikey", password);
            var request = new RestSharp.RestRequest("/projects", Method.GET);
            IRestResponse response = client.Execute(request);
            var obj = JsonConvert.DeserializeObject<Outer_P>(response.Content);
           
            ObservableCollection<Project> Project = new ObservableCollection<Project>(obj._embedded.elements);
            projectListView.ItemsSource = Project;
      
        }

      
        private void ViewWP_Click(object sender, RoutedEventArgs e)
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

        private void ViewVersion_Click(object sender, RoutedEventArgs e)
        {
            dynamic selected_Project = (Project)projectListView.SelectedItem;
            var project_id = selected_Project.id.ToString();
            var project_name = selected_Project.name.ToString();
            (App.Current as App).project_name = project_name;
            (App.Current as App).project_id = project_id;
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("ViewVersion_Page.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}


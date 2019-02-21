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
using WpfTest.Class.Version;
using Version = WpfTest.Class.Version.Version;

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
    
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            string api_server = (App.Current as App).api_server;
            var client = new RestClient(api_server);
            string password = (App.Current as App).api_key;
            client.Authenticator = new HttpBasicAuthenticator("apikey", password);
            string project_id = (App.Current as App).project_id;
            var request = new RestSharp.RestRequest("/projects/" + project_id + "/versions", Method.GET);
            IRestResponse response = client.Execute(request);
            var obj = JsonConvert.DeserializeObject<RootObject>(response.Content);
            ObservableCollection<Version> Version = new ObservableCollection<Version>(obj._embedded.elements);
            versionListView.ItemsSource = Version;
        }

        private void ViewWP_Click(object sender, RoutedEventArgs e)
        {
            dynamic selected_Version = (Version)versionListView.SelectedItem;
            var version_id = selected_Version.id.ToString();
            var version_name = selected_Version.name.ToString();
            (App.Current as App).version_id = version_id;
            (App.Current as App).version_name = version_name;
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("ViewVersionWP_Page.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}

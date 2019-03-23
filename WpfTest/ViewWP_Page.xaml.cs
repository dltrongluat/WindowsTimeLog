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
using WpfTest.Class.Work_Package;

namespace WpfTest
{
   
    public partial class ViewWP_Page : Page
    {
        public ViewWP_Page()
        {
            InitializeComponent();
        }
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string api_server = (App.Current as App).api_server;
            var client = new RestClient(api_server);
            string password = (App.Current as App).api_key;
            client.Authenticator = new HttpBasicAuthenticator("apikey", password);
            string project_id = (App.Current as App).project_id;
            var endpoint= "/projects/"+ project_id + "/work_packages";
            var request = new RestSharp.RestRequest(endpoint, Method.GET);           
            IRestResponse response = client.Execute(request);
            var obj = JsonConvert.DeserializeObject<Outer_WP>(response.Content);
           
            List<WorkPackage> wp_without_newline = obj._embedded.elements.ConvertAll(
                new Converter<WorkPackage, WorkPackage>(WorkPackage.SubjectWithoutNewline));

            var WP = new ObservableCollection<WorkPackage>();
            foreach (var item in wp_without_newline)
                WP.Add(item);

            wpListView.ItemsSource = WP;
            Title.Text = "Work packages of project: " + (App.Current as App).project_name;
        }

        private void LogTimeMan_Click(object sender, RoutedEventArgs e)
        {

            //get work package id,name from click action
            dynamic selected_WP = (WorkPackage)wpListView.SelectedItem;
            var workpackage_id = selected_WP.id.ToString();
            var workpackage_name = selected_WP.subject.ToString();
           
            (App.Current as App).workpackage_id = workpackage_id;
            (App.Current as App).workpackage_name = workpackage_name;

            //display a new MainWindow
            LogTimeManual_Window LogTimeMan_window = new LogTimeManual_Window();
            LogTimeMan_window.ShowDialog();

           
        }

        private void LogTimeAuto_Click(object sender, RoutedEventArgs e)
        {
            //get work package id,name from click action
            dynamic selected_WP = (WorkPackage)wpListView.SelectedItem;
            var workpackage_id = selected_WP.id.ToString();
            var workpackage_name = selected_WP.subject.ToString();
            (App.Current as App).workpackage_id = workpackage_id;
            (App.Current as App).workpackage_name = workpackage_name;

            LogTimeAuto_Window LogTimeAuto_window = new LogTimeAuto_Window();
            LogTimeAuto_window.ShowDialog();
            
        }
    }
}

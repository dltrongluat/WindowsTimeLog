﻿using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml;
using WpfTest.Class.Work_Package_Version;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for ViewVersionWP_Page.xaml
    /// </summary>
    public partial class ViewVersionWP_Page : Page
    {
        public ViewVersionWP_Page()
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
            string version_name = (App.Current as App).version_name;
            var endpoint = "/projects/"+ project_id + "/work_packages";
            var request = new RestSharp.RestRequest(endpoint, Method.GET);
            IRestResponse response = client.Execute(request);
            var obj = JsonConvert.DeserializeObject<Outer>(response.Content);
            List<WorkPackage> wp_without_newline = obj._embedded.elements.ConvertAll(
              new Converter<WorkPackage, WorkPackage>(WorkPackage.SubjectWithoutNewline));
            var WP = new ObservableCollection<WorkPackage>();
            foreach (var item in wp_without_newline)
                WP.Add(item);
            for (int i = 0; i < WP.Count();)
            {
                if (String.IsNullOrEmpty(WP[i]._links.version.title))
                {
                    //assign emptry string value to prevent null, so filter can work
                    WP[i]._links.version.title = "";
                    i++;
                    continue;
                }
                else
                {
                    i++;
                    continue;
                }
            }
            var filter= WP.Where(WP_item => WP_item._links.version.title.Contains(version_name));
            wpListView.ItemsSource = filter;
            Title.Text = "Work packages of project: " + (App.Current as App).project_name + ", version: " + (App.Current as App).version_name;
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
            //NavigationService nav = NavigationService.GetNavigationService(this);
            //nav.Navigate(new Uri("LogTimeAuto_Page.xaml", UriKind.RelativeOrAbsolute));
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
            //LogTimeManual_Window LogTimeMan_window = new LogTimeManual_Window();
            //LogTimeMan_window.ShowDialog();
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("LogTimeManual_Page.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}

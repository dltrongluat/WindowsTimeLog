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
using System.ComponentModel;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        {
            InitializeComponent();
        }
        public class Outer
        {
            public string _type { get; set; }
            public int total { get; set; }
            public int count { get; set; }
           
            public Embedded2 _embedded { get; set; }

        }
        public class Embedded
        {
            public List<Project> elements { get; set; }
        }
        public class Embedded2
        {
            public List<WorkPackage> elements { get; set; }
        }
        public class WorkPackage
        {
            public string _type { get; set; }
            public string id { get; set; }
            public string subject { get; set; }

            public string spentTime { get; set; }
        }
        public class Project
        {
            
            public string id { get; set; }
            //public string _type { get; set; }
            public string name { get; set; }
            public string identifier { get; set; }
            public string createdAt { get; set; }
          
        }
        //public class Question
        //{
        //    public int id { get; set; }
        //    public string question { get; set; }
        //    public string published_at { get; set; }
        //}

     
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://project.fromlabs.com/");
            client.Authenticator = new HttpBasicAuthenticator("apikey", "12692c9ebcb4ba0dc517b78b1f714cf80edb14ad31819c358d55b614301f8f6f");

            var request = new RestSharp.RestRequest("api/v3/projects/194/work_packages", Method.GET);

            IRestResponse response = client.Execute(request);
            //var content = response.Content;
            //textBox.Text = content;

            var obj = JsonConvert.DeserializeObject<Outer>(response.Content);


           wpListView.ItemsSource = obj._embedded.elements;
           // MessageBox.Show(obj._embedded.elements[0].spentTime);
        }
    }
}

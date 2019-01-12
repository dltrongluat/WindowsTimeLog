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
using System.Text.RegularExpressions;
using DataFormat = RestSharp.DataFormat;
using System.Net;

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
        



        ////////////////////////////////////Start of post method With simple post api from pollsapi.docs.apiary.io///////////////////////


        public class Question
        {
            public int id { get; set; }           
            public string published_at { get; set; }
            public List<Choice> choices { get; set; }
            public string question { get; set; }
              
        }
        public class Question_POST
        {
            public int id { get; set; }
            public string published_at { get; set; }
            public string question { get; set; }
            public List<string> choices { get; set; }
        }

        public class Choice
        {
            public string url { get; set; }
            public int votes { get; set; }
            public string choice { get; set; }
            
        }
        private void Get_Clickk(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://polls.apiblueprint.org/");
            var request = new RestSharp.RestRequest("questions", Method.GET);
            IRestResponse response = client.Execute<Question>(request);
            var myobjlist = JsonConvert.DeserializeObject<List<Question>>(response.Content);


            textBox.Text = myobjlist[0].id + myobjlist[0].question + myobjlist[0].published_at + myobjlist[0].choices[1].choice+myobjlist[0].choices[0].choice;

        }
        private void Post_Clickk(object sender, RoutedEventArgs e)
        {

            var test = new Question_POST { question ="22222", choices = new List<string> { "333", "444" } };

            var json = JsonConvert.SerializeObject(test);

            var client = new RestClient("https://polls.apiblueprint.org/");

            var request = new RestRequest("questions", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(json);
           
            IRestResponse response = client.Execute(request);

        }

        ///end of post method///
    }
}

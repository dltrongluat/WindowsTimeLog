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
        ///////////////////////////////////////Start of get///////////////
        //public class Outer
        //{
        //    public string _type { get; set; }
        //    public int total { get; set; }
        //    public int count { get; set; }

        //    public EmbeddedWorkPackage _embedded { get; set; }

        //}
        //public class EmbeddedProject
        //{
        //    public List<Project> elements { get; set; }
        //}
        //public class EmbeddedWorkPackage
        //{
        //    public List<WorkPackage> elements { get; set; }
        //}
        //public class WorkPackage
        //{
        //    public string _type { get; set; }
        //    public string id { get; set; }
        //    public string subject { get; set; }

        //    public string spentTime { get; set; }

        //    public static WorkPackage SubjectWithoutNewline(WorkPackage wp)
        //    {
        //        WorkPackage new_wp = new WorkPackage()
        //        {
        //            _type = wp._type,
        //            id = wp.id,
        //            subject = Regex.Replace(wp.subject, @"\t|\n|\r", ""),
        //            spentTime = Regex.Replace(wp.spentTime,@"PT","")

        //        };

        //        return new_wp;
        //    }
        //}
        //public Uri Source { get; set; }
        //public class Project
        //{                                                                                                                                                   

        //    public string id { get; set; }
        //    //public string _type { get; set; }
        //    public string name { get; set; }
        //    public string identifier { get; set; }
        //    public string createdAt { get; set; }

        //}



        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var client = new RestClient("https://project.fromlabs.com/");
        //    client.Authenticator = new HttpBasicAuthenticator("apikey", "12692c9ebcb4ba0dc517b78b1f714cf80edb14ad31819c358d55b614301f8f6f");

        //    var request = new RestSharp.RestRequest("api/v3/projects/194/work_packages", Method.GET);

        //    IRestResponse response = client.Execute(request);
        //    //var content = response.Content;
        //    //textBox.Text = content;

        //    var obj = JsonConvert.DeserializeObject<Outer>(response.Content);

        //    List<WorkPackage> wp_without_newline = obj._embedded.elements.ConvertAll(
        //        new Converter<WorkPackage, WorkPackage>(WorkPackage.SubjectWithoutNewline));

        //    wpListView.ItemsSource = wp_without_newline;

        //    // MessageBox.Show(obj._embedded.elements[0].spentTime);
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //}

        /////////////////////////////end of get method ///////////////





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

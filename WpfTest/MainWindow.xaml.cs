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

        
        public class Question
        {
            public int id { get; set; }
            public string question { get; set; }
            public string published_at { get; set; }
        }
        private void Go_Clickk(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://polls.apiblueprint.org/");
           // client.Authenticator = new HttpBasicAuthenticator("admin","0123456789");
      
            var request = new RestSharp.RestRequest("questions", Method.GET);

            
           IRestResponse response = client.Execute<Question>(request);
            //textBox.Text = content;



            var myobjlist = JsonConvert.DeserializeObject<List<Question>>(response.Content);
            var myobj = myobjlist[0];

            textBox.Text = myobjlist[0].id + myobjlist[0].question + myobjlist[0].published_at;

            //textBox.Text = myobjlist.Cast<Question>().ToArray();
            // Console.WriteLine(a.question);
            //MessageBox.Show(a.ToString());
            //MessageBox.Show("Sds");

        }
    }
}

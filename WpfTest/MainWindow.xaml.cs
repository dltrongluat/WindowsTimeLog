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
      
            var request = new RestRequest("questions", Method.GET);

            //IRestResponse response = client.Execute(request);
            IRestResponse response = client.Execute<Question>(request);
            //var content = response.Content;
            //textBox.Text = content;
            var o = JsonConvert.DeserializeObject<Question>(json);
            
        }
    }
}

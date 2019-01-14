using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RestSharp;
using RestSharp.Authenticators;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        public static class Globals
        {
            public static string password ;
        }
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string api_server = "https://luattest.openproject.com/api/v3";
            string username = "apikey";
            string password = API_Key.Text;
            var client = new RestClient(api_server);
            client.Authenticator = new HttpBasicAuthenticator(username, password);
            //  get
            var request = new RestRequest("users/me", Method.GET);
            //  add header
            request.AddHeader("Content-Type", "application/json");
            //  execute request
            IRestResponse response = client.Execute(request);
            //  get status code of the response
            HttpStatusCode statusCode = response.StatusCode;
            int numbericStatusCode = (int)statusCode;
            if (numbericStatusCode != 200)
            {
                MessageBox.Show("Authenticate failed!");
                
            }
            else
            {
                MessageBox.Show("Authenticate success!");
                Main.Content = new ViewProject_Page();
            }
        }

    }
}

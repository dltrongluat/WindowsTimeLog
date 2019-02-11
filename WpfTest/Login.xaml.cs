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
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    /// 

   
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        class User
        {
            public string id { get; set; }
        }
       
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string api_server = "https://luattest2.openproject.com/api/v3";
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

            //get User id 
            var obj = JsonConvert.DeserializeObject<User>(response.Content);
         

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
                //store user id for log time function
                (App.Current as App).u_id = obj.id;
                // display view project page
                Main.Content = new ViewProject_Page();
            }
        }

    }
   

}

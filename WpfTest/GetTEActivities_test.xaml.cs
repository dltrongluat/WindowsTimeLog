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
    /// Interaction logic for GetTEActivities_test.xaml
    /// </summary>
    public partial class GetTEActivities_test : Window
    {
        public GetTEActivities_test()
        {
            InitializeComponent();
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string api_server = " https://luattest.openproject.com/api/v3/time_entries/";
            string username = "apikey";
            string password = API_Key.Text;
           
            //  get
            var tea_id = 1;
            var count = 1;

            bool dieukien = true;
            while (dieukien == true)
            {

                var client = new RestClient(api_server);

                client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("activities/" + tea_id, Method.GET);
                //  add header
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
                //  get status code of the response
                HttpStatusCode statusCode = response.StatusCode;
                int numbericStatusCode = (int)statusCode;
                count++;
                if (numbericStatusCode !=200)
                {
                    dieukien = false;
                    break;
                }

              }
                Text.Text = "total activities: " + count.ToString() + "dk" + dieukien.ToString();
            //MessageBox.Show(count.ToString());

            
        }
    }
}

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
    /// Interaction logic for GetTEActivities_test.xaml
    /// </summary>
    public partial class GetTEActivities_test : Window
    {
        public GetTEActivities_test()
        {
            InitializeComponent();
        }
        public class RootObject
        {
            public string _type { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int position { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string api_server = "https://luattest.openproject.com/api/v3/time_entries/";
            string username = "apikey";
            string password = API_Key.Text;
            //  get
            var tea_id = 1;
            bool dieukien = true;
            while (dieukien == true)
            {

                var client = new RestClient(api_server);   
                client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("activities/" + tea_id, Method.GET);
         
                IRestResponse response = client.Execute(request);
                var obj = JsonConvert.DeserializeObject<RootObject>(response.Content);
      
                //  get status code of the response
                HttpStatusCode statusCode = response.StatusCode;
                int numbericStatusCode = (int)statusCode;
  
                if (numbericStatusCode !=200)
                {
                    tea_id = tea_id - 1;
                    dieukien = false;
                    break;
                }
                f1.Text = "total activities valid:  " + tea_id.ToString() + "  dk" + dieukien.ToString();
                tea_id++;
            }
        }
    }
}

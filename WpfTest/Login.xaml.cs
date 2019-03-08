﻿using System;
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
using System.Windows.Forms;
using MahApps.Metro.Controls;
using MessageBox = System.Windows.Forms.MessageBox;
using Application = System.Windows.Application;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfTest
{

    public partial class Login : MetroWindow
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

         

            string api_server = (App.Current as App).api_server;
            string username = "apikey";
            string password = API_Key.Text;
            var client = new RestClient(api_server);
            client.Authenticator = new HttpBasicAuthenticator(username, password);
            //  get
            var request = new RestRequest("/users/me", Method.GET);
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
                (App.Current as App).api_key = password;
           
                // display frame window
            
                FrameWindow window = new FrameWindow();
                window.Show();
                this.Close();
              
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string file_path = @"D:\New folder\WpfTest\WpfTest\Test.txt";
            List<Setting> setting = new List<Setting>();
            List<string> lines = File.ReadAllLines(file_path).ToList();
            foreach (var  line in lines)
            {
                string[] entries = line.Split(',');
                Setting new_setting = new Setting();
                new_setting.id = entries[0];
                new_setting.href = entries[1];
                setting.Add(new_setting);
            }
            string api_server = setting[0].href.ToString();
            string api_mockup_server = setting[1].href.ToString();
            (App.Current as App).api_server = api_server;
            (App.Current as App).api_mockup_server = api_mockup_server;
        }


    }
   

}

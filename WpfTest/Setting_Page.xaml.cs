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
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using Path = System.IO.Path;
using System.Threading;
using System.Collections.ObjectModel;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using System.Net;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for Setting_Page.xaml
    /// </summary>
    public partial class Setting_Page : Page
    {
        public ObservableCollection<App.TE_Settingg> Setting { get; set; }
        private static object _syncLock = new object();

        //validate
        private int _noOfErrorsOnScreen = 0;
        public TE_Setting_Validate TE_Setting_Validate = new TE_Setting_Validate();

        public Setting_Page()
        {
            InitializeComponent();
            ReadFile();
            FileWatherConfigure();
            grid.DataContext = TE_Setting_Validate;

        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _noOfErrorsOnScreen++;
            else
                _noOfErrorsOnScreen--;
        }

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _noOfErrorsOnScreen == 0;
            e.Handled = true;
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TE_Setting_Validate cust = grid.DataContext as TE_Setting_Validate;
            // reset UI
            TE_Setting_Validate = new TE_Setting_Validate();

            e.Handled = true;

        }




        public class TE_Setting
        {
            public string id { get; set; }
            public string name { get; set; }
        }
        public class TE_Activity
        {

            public string id { get; set; }
            public string name { get; set; }

        }
        FileSystemWatcher fileWatcher = new FileSystemWatcher();
        public void ReadFile()
        {
            Setting = new ObservableCollection<App.TE_Settingg>();
            BindingOperations.EnableCollectionSynchronization(Setting, _syncLock);
            ///grid.DataContext = TE_Setting_Validate;
            var directory = System.AppDomain.CurrentDomain.BaseDirectory;
            //string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
            var file = Path.Combine(directory, "TE_Activities.txt");
            List<string> lines = File.ReadAllLines(file.ToString()).ToList();
            //List<App.TE_Settingg> Setting = new List<App.TE_Settingg>();
            foreach (var line in lines.Select((x, i) => new { Value = x, Index = i }))
            {
                string[] entries = line.Value.Split(',');

                App.TE_Settingg new_setting = new App.TE_Settingg()
                {
                    id = entries[0],
                    name = entries[1]
                };
                Setting.Add(new_setting);

            }
            (App.Current as App).elements = Setting;

            listBox.Dispatcher.Invoke(() => { this.listBox.ItemsSource = Setting; });


        }


        public void FileWatherConfigure()
        {
            var directory = System.AppDomain.CurrentDomain.BaseDirectory;
            //string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
            var file = Path.Combine(directory, "TE_Activities.txt");
            fileWatcher.Path = System.IO.Path.GetDirectoryName(file);
            fileWatcher.Filter = System.IO.Path.GetFileName(file);
            fileWatcher.Changed += FileWatcher_Changed;
            fileWatcher.EnableRaisingEvents = true;
        }
        private void FileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));

            ReadFile();
        }


        public void ValidateOnInsert()
        {
            ObservableCollection<App.TE_Settingg> Setting = (App.Current as App).elements;

            App.TE_Settingg new_setting = new App.TE_Settingg()
            {
                id = tb_ID.Text,
                name = tb_Subject.Text
            };
            Setting.Add(new_setting);
            WriteFile();
        }
        public void Insert_Click(object sender, RoutedEventArgs e)
        {
            string username = "apikey";
            string api_key = (App.Current as App).api_key;
            string api_server = (App.Current as App).api_server;
            var client = new RestClient(api_server);
            client.Authenticator = new HttpBasicAuthenticator(username, api_key);

            //  get
            var request = new RestRequest("/time_entries/activities/" + tb_ID.Text, Method.GET);
            //  add header
            request.AddHeader("Content-Type", "application/json");
            //  execute request
            IRestResponse response = client.Execute(request);
            //get User id 

            HttpStatusCode statusCode = response.StatusCode;
            int numbericStatusCode = (int)statusCode;
            if (numbericStatusCode != 200)
            {
                MessageBox.Show("Something went wrong");
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<TE_Activity>(response.Content);
                if (tb_ID.Text == obj.id)
                {
                    ValidateOnInsert();
                }

            }


        }
        public void Update_Click(object sender, RoutedEventArgs e)
        {

            //dynamic selected_Project = (App.TE_Settingg)listBox.SelectedItem;
            //var project_id = selected_Project.id.ToString();
            int index = listBox.SelectedIndex + 1;
            var directory = System.AppDomain.CurrentDomain.BaseDirectory;
            //string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
            var file = Path.Combine(directory, "TE_Activities.txt");


            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(file))
            {
                int Countup = 0;
                while (!sr.EndOfStream)
                {
                    Countup++;
                    if (Countup != index)
                    {
                        using (StringWriter sw = new StringWriter(sb))
                        {
                            sw.WriteLine(sr.ReadLine());
                        }
                    }
                    else
                    {
                        sr.ReadLine();
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.Write(sb.ToString());
            }
        }
        private void WriteFile()
        {
            ObservableCollection<App.TE_Settingg> Setting = (App.Current as App).elements;
            var directory = System.AppDomain.CurrentDomain.BaseDirectory;
            //string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
            var file = Path.Combine(directory, "TE_Activities.txt");
            List<string> lines = File.ReadAllLines(file.ToString()).ToList();


            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < Setting.Count; i++)
            {
                App.TE_Settingg new_setting = (App.TE_Settingg)Setting[i];
                strBuilder.Append(new_setting.id + "," + new_setting.name + Environment.NewLine);
            }
            File.WriteAllText(file, strBuilder.ToString());


        }

    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.IO;
//using Microsoft.Win32;
//using System.Reflection;
//using Path = System.IO.Path;
//using System.Threading;

//using System.Collections.ObjectModel;
//using RestSharp;
//using RestSharp.Authenticators;
//using Newtonsoft.Json;
//using System.Net;

//namespace WpfTest
//{
//    /// <summary>
//    /// Interaction logic for Setting_Page.xaml
//    /// </summary>
//    public partial class Setting_Page : Page
//    {
//        public ObservableCollection<App.TE_Settingg> Setting { get;  set; }
//        private static object _syncLock = new object();


//        public Setting_Page()
//        {
//            InitializeComponent();
//            ReadFile();
//            FileWatherConfigure();



//        }
//        public class TE_Setting
//        {
//            public string id { get; set; }
//            public string name { get; set; }
//        }
//        public class TE_Activity
//        {

//            public string id { get; set; }
//            public string name { get; set; }

//        }
//        FileSystemWatcher fileWatcher = new FileSystemWatcher();
//        public void ReadFile()
//        {
//            Setting = new ObservableCollection<App.TE_Settingg>();
//            BindingOperations.EnableCollectionSynchronization(Setting, _syncLock);


//            string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
//            var file = Path.Combine(directory, "TE_Activities.txt");
//            List<string> lines = File.ReadAllLines(file.ToString()).ToList();
//            //List<App.TE_Settingg> Setting = new List<App.TE_Settingg>();
//            //ObservableCollection<App.TE_Settingg> Setting = (App.Current as App).elements;
//            foreach (var line in lines.Select((x, i) => new { Value = x, Index = i }))
//            {
//                string[] entries = line.Value.Split(',');

//                App.TE_Settingg new_setting = new App.TE_Settingg()
//                {
//                    id = entries[0],
//                    name = entries[1]
//                };

//                    Setting.Add(new_setting);



//            };
//            (App.Current as App).elements = Setting;

//            listBox.Dispatcher.Invoke(() => { this.listBox.ItemsSource = Setting; });


//        }


//        public void FileWatherConfigure()
//        {
//            string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
//            var file = Path.Combine(directory, "TE_Activities.txt");
//            fileWatcher.Path = System.IO.Path.GetDirectoryName(file);
//            fileWatcher.Filter = System.IO.Path.GetFileName(file);
//            fileWatcher.Changed += FileWatcher_Changed;
//            fileWatcher.EnableRaisingEvents = true;
//        }
//        private void FileWatcher_Changed(object sender, FileSystemEventArgs e)
//        {
//            Thread.Sleep(TimeSpan.FromSeconds(1));

//            ReadFile();
//        }

//        private void Page_Loaded(object sender, RoutedEventArgs e)
//        {

//        }
//        public void ValidateOnInsert()
//        {
//            ObservableCollection<App.TE_Settingg> Setting = (App.Current as App).elements;

//            App.TE_Settingg new_setting = new App.TE_Settingg()
//            {
//                id = ID.Text,
//                name = Subject.Text
//            };
//            Setting.Add(new_setting);
//            WriteFile();
//        }
//        public void Insert_Click(object sender, RoutedEventArgs e)
//        {
//            string username = "apikey";
//            string api_key = (App.Current as App).api_key;
//            string api_server = (App.Current as App).api_server;
//            var client = new RestClient(api_server);
//            client.Authenticator = new HttpBasicAuthenticator(username, api_key);

//            //  get
//            var request = new RestRequest("/time_entries/activities/" + ID.Text, Method.GET);
//            //  add header
//            request.AddHeader("Content-Type", "application/json");
//            //  execute request
//            IRestResponse response = client.Execute(request);
//            //get User id 

//            HttpStatusCode statusCode = response.StatusCode;
//            int numbericStatusCode = (int)statusCode;
//            if (numbericStatusCode != 200)
//            {
//                MessageBox.Show("Something went wrong");
//            }
//            else
//            {
//                var obj = JsonConvert.DeserializeObject<TE_Activity>(response.Content);
//                if (ID.Text == obj.id)
//                {
//                    ValidateOnInsert();
//                }

//            }


//        }

//        private void WriteFile()
//        {
//            ObservableCollection<App.TE_Settingg> Setting = (App.Current as App).elements;
//            string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
//            var file = Path.Combine(directory, "TE_Activities.txt");
//            List<string> lines = File.ReadAllLines(file.ToString()).ToList();


//            StringBuilder strBuilder = new StringBuilder();

//            for (int i = 0; i < Setting.Count; i++)
//            {
//                App.TE_Settingg new_setting = (App.TE_Settingg)Setting[i];
//                strBuilder.Append(new_setting.id + "," + new_setting.name + Environment.NewLine);
//            }
//            File.WriteAllText(file, strBuilder.ToString());


//        }

//    }
//}


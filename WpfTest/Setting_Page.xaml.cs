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
using System.Windows.Navigation;
using System.Threading;
using System.Collections.ObjectModel;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for Setting_Page.xaml
    /// </summary>
    public partial class Setting_Page : Page
    {
        public Setting_Page()
        {
            InitializeComponent();
            ReadFile();
            FileWatherConfigure();
         
             
        }
        public class TE_Setting
        {
            public string id { get; set; }
            public string name { get; set; }
        }
        //public class WTF
        //{
        //    public TE_Setting Obj;
          
        //    public void SetObject(TE_Setting obj)
        //    {
        //        Obj = obj;
        //    }
        //}


        FileSystemWatcher fileWatcher = new FileSystemWatcher();
        public void ReadFile()
        {
           
            string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
            var file = Path.Combine(directory, "TE_Activities.txt");
            List<string> lines = File.ReadAllLines(file.ToString()).ToList();
            List<App.TE_Settingg> Setting = new List<App.TE_Settingg>();
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

      

        //List<App.TE_Settingg> Setting = new List<App.TE_Settingg>();
        public void FileWatherConfigure()
        {
            string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
        
        public void Insert_Click(object sender, RoutedEventArgs e)
        {
            List <App.TE_Settingg> Setting = (App.Current as App).elements;

            App.TE_Settingg new_setting = new App.TE_Settingg()
            {
                id = ID.Text,
                name = Subject.Text
            };
            Setting.Add(new_setting);
            WriteFile();


            //var directory2 = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //var file = Path.Combine(directory2, "TE_Activities.txt");
            //string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
            //MessageBox.Show(directory.ToString()+ "++" + file.ToString());
            //TE_Insert_Window Window = new TE_Insert_Window(Setting);
            //Window.ShowDialog();

        }

        private void WriteFile()
        {
            List<App.TE_Settingg> Setting = (App.Current as App).elements;
            string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
            var file = Path.Combine(directory, "TE_Activities.txt");
            List<string> lines = File.ReadAllLines(file.ToString()).ToList();
          
       
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i <Setting.Count; i++)
            {
                App.TE_Settingg new_setting = (App.TE_Settingg)Setting[i];
                strBuilder.Append(new_setting.id + "," + new_setting.name + Environment.NewLine);
            }
            File.WriteAllText(file, strBuilder.ToString());

          
        }
       
    }
}

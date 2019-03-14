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
        }
        List<App.TE_Settingg> Setting = new List<App.TE_Settingg>();
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //TE_Insert_Page page = new TE_Insert_Page();
           
            //App.TE_Settingg new_setting = new App.TE_Settingg(ID.Text, Subject.Text)
            //{
            //    id = ID.Text,
            //    name = Subject.Text
            //};  
            //listBox.Items.Add(new_setting);
            //Setting.Add(new_setting);



        }
        //public List<TE_Setting> Setting = new List<TE_Setting>();

      
     
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var file = Path.Combine(directory, "TE_Activities.txt");
            //List<TE_Setting> setting = new List<TE_Setting>();

            List<string> lines = File.ReadAllLines(file).ToList();
            foreach (var line in lines.Select((x, i) => new { Value = x, Index = i }))
            {
                string[] entries = line.Value.Split(',');

                App.TE_Settingg new_setting = new App.TE_Settingg(entries[0], entries[1])
                {
                    id = entries[0],
                    name = entries[1]
                };
                Setting.Add(new_setting);
      

            }
            //(App.Current as App).elements = Setting;
            (App.Current as App).elements = Setting ;
            listBox.ItemsSource = Setting;
        }

        //private void Insert_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void Update_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}

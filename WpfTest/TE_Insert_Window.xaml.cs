using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Path = System.IO.Path;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for TE_Insert_Window.xaml
    /// </summary>
    public partial class TE_Insert_Window : Window
    {
        public TE_Insert_Window()
        {
            InitializeComponent();
        }
        //List<App.TE_Settingg> Setting = new List<App.TE_Settingg>();
        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            App.TE_Settingg new_setting = new App.TE_Settingg(ID.Text, Subject.Text)
            {
                id = ID.Text,
                name = Subject.Text
            };
            //Setting.Add(new_setting);
            (App.Current as App).elements.Add(new_setting);

            
            Setting_Window.datagrid.ItemsSource = (App.Current as App).elements.ToList();
            WriteFile();
            this.Hide();
           
        
        }
        private void WriteFile()
        {
            string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
            //var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var file = Path.Combine(directory, "TE_Activities.txt");
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < Setting_Window.datagrid.Items.Count; i++)
            {
                App.TE_Settingg prsn = (App.TE_Settingg)Setting_Window.datagrid.Items[i];
                strBuilder.Append(prsn.id+","+prsn.name+Environment.NewLine);
            }
            File.WriteAllText(file, strBuilder.ToString());
            //Setting_Page.datagrid.ItemsSource = (App.Current as App).elements;

            //foreach (App.TE_Settingg author in (Setting_Page.datagrid.ItemsSource))
            //{
            //    MessageBox.Show(author.name);
            //}


        }
    }
}

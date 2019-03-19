using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
        public TE_Insert_Window(List<App.TE_Settingg> Setting )
        {
            InitializeComponent();
        }
        List<App.TE_Settingg> Setting = new List<App.TE_Settingg>();
        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            //List<App.TE_Settingg> Setting = new List<App.TE_Settingg>();
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
            //WriteFile();
            //this.Hide();


        }
      
        
        private void WriteFile()
        {
            //string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();

            //var file = Path.Combine(directory, "TE_Activities.txt");
            //StringBuilder strBuilder = new StringBuilder();

            //for (int i = 0; i < Setting_Page.datagrid.Items.Count; i++)
            //{
            //    App.TE_Settingg prsn = (App.TE_Settingg)Setting_Page.datagrid.Items[i];
            //    strBuilder.Append(prsn.id+","+prsn.name+Environment.NewLine);
            //}
            //File.WriteAllText(file, strBuilder.ToString());
          

        }
    }
}

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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using Path = System.IO.Path;

using System.Windows.Navigation;
namespace WpfTest
{
   
    public partial class Setting_Window : Window
    {
        public Setting_Window()
        {
            InitializeComponent();
        }

        List<App.TE_Settingg> Setting = new List<App.TE_Settingg>();
        public static DataGrid datagrid;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
           

            var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var file = Path.Combine(directory, "TE_Activities.txt");
        
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
            (App.Current as App).elements = Setting;
            listBox.ItemsSource = Setting;
            datagrid = listBox;

        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            TE_Insert_Window window = new TE_Insert_Window();

            window.ShowDialog();

        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
           



        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
           



        }
        //private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Multiselect = true;
        //    var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        //    var file = Path.Combine(directory, "Test.txt");
        //    openFileDialog.InitialDirectory = directory;
        //    openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        //    if (openFileDialog.ShowDialog() == true)
        //        foreach (string filename in openFileDialog.FileNames)
        //            lbFiles.Items.Add(Path.GetFileName(filename));
        //}
        //private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    if (saveFileDialog.ShowDialog() == true)
        //        File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
        //}
    }
}

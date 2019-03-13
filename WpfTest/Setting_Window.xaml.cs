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

namespace WpfTest
{
   
    public partial class Setting_Window : Window
    {
        public Setting_Window()
        {
            InitializeComponent();
        }
      
        public List<TE_Setting> Setting = new List<TE_Setting>();
        //public class TE_Setting
        //{
        //    public string id { get; set; }
        //    public string name { get; set; }

        //}
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var file = Path.Combine(directory, "TE_Activities.txt");

            //List<TE_Setting> setting = new List<TE_Setting>();

           
            List<string> lines = File.ReadAllLines(file).ToList();
            foreach (var line in lines)
            {
                string[] entries = line.Split(',');
                TE_Setting new_setting = new TE_Setting();
                new_setting.id = entries[0];
                new_setting.name = entries[1];
                Setting.Add(new_setting);
            }
           
            
            listBox.ItemsSource = Setting;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TEActivity_Window window = new TEActivity_Window();
            window.Show();
           

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

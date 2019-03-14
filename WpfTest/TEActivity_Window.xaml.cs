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

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for TEActivity_Window.xaml
    /// </summary>
    public partial class TEActivity_Window : Window
    {
        public TEActivity_Window()
        {
            InitializeComponent();
        }
        public List<TE_Setting> Setting = new List<TE_Setting>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
       
          
            //Setting_Page page1 = new Setting_Page();
            //this.Content = page1;
            //this.NavigationService.Refresh();
        }


        private void Add_Click_1(object sender, RoutedEventArgs e)
        {
            //App.TE_Settingg wtf = (App.Current as App).elements;

            ////App.TE_Settingg new_setting = new App.TE_Settingg(ID.Text, Subject.Text)
            ////{
            ////    id = ID.Text,
            ////    name = Subject.Text
            ////};
            ////Setting.id = ID.Text;
            ////Setting.name = Subject.Text;
            //Setting.Add(new_setting);
            //MessageBox.Show("complete");
            //listBox.ItemsSource = Setting;
        }
    }
}

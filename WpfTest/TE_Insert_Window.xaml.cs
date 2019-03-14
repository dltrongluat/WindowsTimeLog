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
            this.Hide();
            //MessageBox.Show((App.Current as App).elements[3].name);
        }
    }
}

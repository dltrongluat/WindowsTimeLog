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
           
         TE_Setting new_setting = new TE_Setting();
            new_setting.id = ID.Text;
            new_setting.name = Subject.Text;
            Setting.Add(new_setting);
            //this.NavigationService.Refresh();
        }
    }
}

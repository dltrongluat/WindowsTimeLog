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
    /// Interaction logic for NavWindow.xaml
    /// </summary>
    public partial class NavWindow : Window
    {
        public NavWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ViewWP_Page();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ViewWP_Page();
        }
    }
}

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
using MahApps.Metro.Controls;
using MessageBox = System.Windows.Forms.MessageBox;
namespace WpfTest
{
    /// <summary>
    /// Interaction logic for FrameWindow.xaml
    /// </summary>
    public partial class FrameWindow : MetroWindow
    {
        public FrameWindow()
        {
            InitializeComponent();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Content = new ViewProject_Page();
            
        }
    }
}

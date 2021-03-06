﻿using System;
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
            //Main.Content = new Setting_Page();

        }
        //Slider
        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpen.Visibility = Visibility.Collapsed;
            ButtonClose.Visibility = Visibility.Visible;  
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpen.Visibility = Visibility.Visible;
            ButtonClose.Visibility = Visibility.Collapsed;

        }

        private void ViewProject_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ViewProject_Page();
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Setting_Page();
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
          
        }
    }
}

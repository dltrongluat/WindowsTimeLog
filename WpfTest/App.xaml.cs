using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string project_id { get; set; }
        public string project_name { get; set; }


        public string workpackage_id { get; set; }

        public string workpackage_name { get; set; }
        public string activity_id { get; set; }
        public string activity_name { get; set; }

        public string u_id { get; set; }

        
    }
  
}

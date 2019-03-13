using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest
{
    public class Setting
    {
        public string id { get; set; }
        public string href { get; set; }

    }
    //public List<TE_Setting> Setting = new List<TE_Setting>();
    public class TE_Setting
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}

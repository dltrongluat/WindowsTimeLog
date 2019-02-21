using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.Class
{
   
        public class Outer_P
        {
            public string _type { get; set; }
            public int total { get; set; }
            public int count { get; set; }
            public EmbeddedProject _embedded { get; set; }
        }
        public class EmbeddedProject
        {
            public List<Project> elements { get; set; }
        }
        public class Project
        {
            public string id { get; set; }
            public string name { get; set; }
            public string identifier { get; set; }
            public string createdAt { get; set; }

        }
    
}

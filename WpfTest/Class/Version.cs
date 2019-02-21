using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.Class.Version
{
    public class Self
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class DefiningProject
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public DefiningProject definingProject { get; set; }
    }

    public class Version
    {
        public int id { get; set; }
        public string name { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string status { get; set; }
        public Links _links { get; set; }
    }

    public class Embedded
    {
        public List<Version> elements { get; set; }
    }

    public class RootObject
    {
        public Embedded _embedded { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace WpfTest.Class.Work_Package
{
    public class Outer_WP
    {
        public string _type { get; set; }
        public int total { get; set; }
        public int count { get; set; }

        public EmbeddedWorkPackage _embedded { get; set; }
    }

    public class EmbeddedWorkPackage
    {
        public List<WorkPackage> elements { get; set; }
    }
    public class WorkPackage
    {

        public string id { get; set; }
        public string subject { get; set; }

        public string spentTime { get; set; }
        public Links _links { get; set; }

        public static WorkPackage SubjectWithoutNewline(WorkPackage wp)
        {
            WorkPackage new_wp = new WorkPackage()
            {
                id = wp.id,
                subject = Regex.Replace(wp.subject, @"\t|\n|\r", ""),
                //TimeSpan aaa = XmlConvert.ToTimeSpan(obj._embedded.elements[0].spentTime)
                spentTime = XmlConvert.ToTimeSpan(wp.spentTime).TotalHours.ToString() + "H",
                _links = wp._links
            };
            return new_wp;
        }

    }
    public class Links
    {
        public Version version { get; set; }
    }
    public class Version
    {
        public string href { get; set; }
        public string title { get; set; }
    }
}

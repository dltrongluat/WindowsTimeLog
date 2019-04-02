using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.Class.Log_Time
{
    public class LinksProperty
    {
        public string href { get; set; }
    }
    // create a class of combobox activity type
    public class ComboBoxActivity
    {
        public string key { get; set; }
        public string value { get; set; }
        public ComboBoxActivity(string _key, string _value)
        {
            key = _key;
            value = _value;
        }
    }
    public class Links
    {
        public LinksProperty project { get; set; }
        public LinksProperty activity { get; set; }
        public LinksProperty workPackage { get; set; }
        public LinksProperty customField4 { get; set; }
    }
    public class RootObject
    {
        public Links _links { get; set; }
        public string hours { get; set; }
        public string comment { get; set; }
        public string spentOn { get; set; }
    }

    // class for time entries activities object for mockup api
    public class TE_Activity
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class RootObjectActivity
    {
        public List<TE_Activity> activities { get; set; }
    }
}

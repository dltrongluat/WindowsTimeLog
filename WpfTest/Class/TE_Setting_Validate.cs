using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
namespace WpfTest
{
    public class TE_Setting_Validate : IDataErrorInfo
    {
        public string ID { get; set; }
        public string Subject { get; set; }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Subject")
                {
                    if (string.IsNullOrEmpty(Subject))
                        result = "Please enter a time entry activity";
                    else if (!Regex.IsMatch(Subject,@"^[a-z0-9]"))
                        result = "Please enter a valid time entry";

                }
                if (columnName == "ID")
                {

                    if (string.IsNullOrEmpty(ID))
                        result = "Please enter log hour";

                    else if (!Regex.IsMatch(ID, @"^[0-9]*(?:\.[0-9]*)?$"))
                        result = "Please enter a valid number";
                  
                }

                return result;
            }
        }
    }
}

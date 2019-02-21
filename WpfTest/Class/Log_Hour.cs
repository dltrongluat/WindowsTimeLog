using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WpfTest
{

   //use for validations
    public class Log_Hour : IDataErrorInfo
    {
        public string LogHour { get; set; }
        public string Comment { get; set; }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Comment")
                {
                    if (string.IsNullOrEmpty(Comment))
                        result = "Please enter comment";
                    
                }
                if (columnName == "LogHour")
                {

                    if (string.IsNullOrEmpty(LogHour))
                        result = "Please enter log hour";

                    else if (!Regex.IsMatch(LogHour, @"^[0-9]*(?:\.[0-9]*)?$"))
                        result = "Please enter a valid number";
                    else if (float.Parse(LogHour, CultureInfo.InvariantCulture.NumberFormat) == 0)
                        result = "Must not = 0";
                }
            
                return result;
            }
        }


    }
}

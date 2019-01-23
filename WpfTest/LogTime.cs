using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace WpfTest
{

  
    public class LogTime : IDataErrorInfo
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
                }
            
                return result;
            }
        }


    }
}

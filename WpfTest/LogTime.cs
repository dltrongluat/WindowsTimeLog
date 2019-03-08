using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WpfTest
{
    public class LogTime : IDataErrorInfo
    {
        private string _loghour;
        [Required(ErrorMessage = "log hour is required")]
        public string loghour
        {
            get;
            set;
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }
        //string IDataErrorInfo.this[string propertyName] // Part of the IDataErrorInfo Interface
        //{
        //    get { return OnValidate(propertyName) }
        //}
        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName =="loghour")
                {
                    if (String.IsNullOrEmpty(loghour))
                    {
                        result ="please enter loghour";
                    }
                }

                return result;
            }
        }
    }

}

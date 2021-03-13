using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courses.Models
{
    public class UserClass 
    { 
        public string _IDUser { get; set; }
        public string _Name { get; set; }
        public Nullable<DateTime> _DOB { get; set; }
        public Nullable<int> _TypeUser { get; set; }
        public string _IDClass { get; set; }
        public string _ClassName { get; set; }
    }
}
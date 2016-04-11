using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDemo.Models
{
    public class CompanyRepresentative
    {
        public int company_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
        public bool isAlumni { get; set; }
    }
}
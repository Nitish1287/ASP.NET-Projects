using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerFairPlus.Models
{
    public class Job
    {
        public string jobId { get; set; }
        public string company_id { get; set; }
        public string seeking_degree { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string visa_sponsorship { get; set; }
    }
}
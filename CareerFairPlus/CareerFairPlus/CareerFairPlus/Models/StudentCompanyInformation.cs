using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerFairPlus.Models
{
    public class StudentCompanyInformation
    {
        public string notes { get; set; }
        public string isInterested { get; set; }
        public string companyName { get; set; }
        public List<Job> jobs = new List<Job>();
        public string additionalNotes { get; set; }
        public bool inQueue { get; set; }
    }
}
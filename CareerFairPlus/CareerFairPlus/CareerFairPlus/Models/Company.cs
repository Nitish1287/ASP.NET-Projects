using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerFairPlus.Models
{
    public class Company
    {
        public string companyId { get; set; }
        public string companyName { get; set; }
        public string sector { get; set; }
        public string headquarters { get; set; }
        public List<Booth> boothList = new List<Booth>();
        public List<Job> jobs = new List<Job>();
    }
}
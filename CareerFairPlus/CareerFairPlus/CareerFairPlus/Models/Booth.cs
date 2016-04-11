using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerFairPlus.Models
{
    public class Booth
    {
        public string boothID { get; set; }
        public string companyId { get; set; }
        public string boothNumber { get; set; }
        public string queueLength { get; set; }
        public string date { get; set; }

    }
}
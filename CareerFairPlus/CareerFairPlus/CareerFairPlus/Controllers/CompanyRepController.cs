using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CareerFairPlus.Models;
using CareerFairPlus.Helpers;
using CareerFairPlus.Interface;

namespace CareerFairPlus.Controllers
{
    public class CompanyRepController : Controller
    {
        
        public ActionResult CompanyRepHomeDetails(string companyID)
        {
            JobsList jobsList = new JobsList();
            ViewBag.CompanyID = companyID;

            return View(jobsList);
        }

        public ActionResult CompanyRepReportGen()
        {
            Reports reports = new Reports();

            return View(reports);
        }

        [HttpGet]
        public ActionResult CompanyRepJobForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CompanyRepJobForm(FormCollection formCollection)
        {
            Job j = new Job();
            JobsList jobsList = new JobsList();
            UserDetailsInterface udInterface = new Helper();
            j.company_id = udInterface.GetIDByUserName(User.Identity.Name, "company_rep").ToString();
            j.seeking_degree = formCollection["seeking_degree"];
            j.title = formCollection["title"];
            j.type = formCollection["type"];
            j.visa_sponsorship = formCollection["visa_sponsorship"];

            jobsList.addJob(j);

            return RedirectToAction("CompanyRepHomeDetails");
        }

    }
}

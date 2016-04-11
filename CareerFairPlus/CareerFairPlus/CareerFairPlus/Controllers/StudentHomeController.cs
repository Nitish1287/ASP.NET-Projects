using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CareerFairPlus.Models;
using CareerFairPlus.Interface;
using CareerFairPlus.Helpers;
using MvcDemo.Models;

namespace CareerFairPlus.Controllers
{
    public class StudentHomeController : Controller
    {
        public ActionResult StudentHomeDetails()
        {
            CompaniesList complist = new CompaniesList();
            
            return View(complist);
        }

        public ActionResult StudentCompanyDetails(string id, string notes = null, string isInterested = null, string additionalNotes = null, bool inQueue = false)
        {
            StudentCompanyDetails compdetails = new StudentCompanyDetails();
            StudentCompanyInformation info = new StudentCompanyInformation();
            UserDetailsInterface udInterface = new Helper();
            if (notes != null)
            {
                info = compdetails.UpdateNotes(id, udInterface.GetIDByUserName(User.Identity.Name, "student"), notes);
            }
            else
                if (additionalNotes != null)
                {
                    info = compdetails.AddNotes(id, udInterface.GetIDByUserName(User.Identity.Name, "student"), additionalNotes);
                }

            if (inQueue)
            {
                info = compdetails.editBoothQueue(id, udInterface.GetIDByUserName(User.Identity.Name, "student"), inQueue);
            }
            else
            {
                info = compdetails.getStudentCompanyDetails(id, udInterface.GetIDByUserName(User.Identity.Name, "student"));
            }

            ViewData["CompanyRepresentative"] = compdetails.GetRepresentativeByCompanyID(Convert.ToInt32(id));
            return View(info);
        }

        public void SendEmail(string emailID)
        {
            
        }
    }
}

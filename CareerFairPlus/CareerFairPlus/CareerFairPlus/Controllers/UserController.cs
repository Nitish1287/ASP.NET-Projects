using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CareerFairPlus.Models;
using MySql.Data.MySqlClient;
using System.Web.Security;

namespace CareerFairPlus.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                
                user.Role = UserContext.IsValid(user.UserName, user.Password);
                if (!user.Role.Equals(null))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                    if (user.Role.Equals("Student"))
                        return RedirectToAction("StudentHomeDetails", "StudentHome");
                    else
                        if (user.Role.Equals("company_rep"))
                            return RedirectToAction("CompanyRepHomeDetails", "CompanyRep");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Models.User user)
        {
            if (ModelState.IsValid)
            {
                if (UserContext.RegisterUser(user.UserName, user.Password, user.Email, user.Role, user.UIN, user.FirstName, user.LastName, user.Major, user.Degree, user.Address, user.Phone))
                {
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Registration Details are incorrect");
                }
            }
            return View(user);
        }
    }
}

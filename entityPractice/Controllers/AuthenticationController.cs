using entityPractice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using entityPractice.helper;
using Database = entityPractice.helper.Database;

namespace entityPractice.Controllers
{
    public class AuthenticationController : Controller
    {
        public object Mailer { get; private set; }

        // GET: Authentication


        [Route("authentication/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("authentication/register")]
        public ActionResult Register()
        {
            // name of the inputs
            string name = Request["name"];
            string email = Request["email"];
            string password = Request["password"];
            string confirm_password = Request["confirm_password"];

            //if the customer does not exist in the data base table
            if (Database.getContext().Customer.SingleOrDefault(m => m.Email == email) == null)
            {
                Customer customer = new Customer()
                {
                    Name = name,
                    Email = email,
                    Password = dHash.make(password),
                    _token = dHash.GetMd5(email),
                };
                Database.getContext().Customer.Add(customer);
                Database.getContext().SaveChanges();
                ViewBag.email = email;
                //Session["Customer"] = customer;
                //Session["LoginSuccess"] = "LoginSuccess";
                dMailer.EmailVerificationMail(email, dHash.GetMd5(email));
                return View("Address");
            }
            else
            {
                Session["UserExists"] = true;
                Response.Write("User Already Exists");
                return RedirectToAction("Index");
            }

        }
    }
}
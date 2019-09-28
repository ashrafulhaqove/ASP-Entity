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
        [Route("authentication/login")]
        public ActionResult Login(FormCollection formCol)
        {
            string email = formCol["email"];
            string password = formCol["password"];
            Customer customer = Database.getContext().Customer.FirstOrDefault(m => m.Email == email);
            if ((customer != null) && (bool)dHash.verify(password, customer.Password))
            {
                Session["Customer"] = customer;
                Session["LoginSuccess"] = "LoginSuccess";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["CustomerError"] = "CustomerError";
            }

            return RedirectToAction("Index", "Authentication");
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
                // inserting in the database
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

        [HttpPost]
        [Route("authentication/address/register")]
        public ActionResult Address(Customer cust, FormCollection formColct)
        {
            string email = formColct["email"];
            Customer customer = Database.getContext().Customer.SingleOrDefault(m => m.Email == email);

            Address address = new Address()
            {
                Customer_Id = customer.Id,
                City = formColct["city"],
                Zip = Convert.ToInt32(formColct["zip"]),
                Details = formColct["details"],
            };

            Database.getContext().Address.Add(address);
            Database.getContext().SaveChanges();
            Session["RgistrationSuccess"] = "RgistrationSuccess";
            Session["Customer"] = customer;
            Session["LoginSuccess"] = "LoginSuccess";
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        [Route("email/verify/{token}")]
        public ActionResult EmailVerify(string token)
        {
            Customer customer = Database.getContext().Customer.FirstOrDefault(m => m._token == token);
            //Response.Write(customer.Name);
            if (customer != null)
            {
                customer._token = null;
                Database.getContext().SaveChanges();
                Session["EmailVerificationSuccess"] = "EmailVerificationSuccess";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Response.Write("token expired!");
            }
            return Content("Something wrong!");

        }



    }
}
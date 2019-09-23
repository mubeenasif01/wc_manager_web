using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using WCManager.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;


namespace WCManager.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAuthenticate(User obj)
        {
            //TODO:Make a class named Getconnection Where GetConnection public method will return static readonly type var of connection string 
            //It's not a good approach your are following.
            string cs = ConfigurationManager.ConnectionStrings["DBconString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * from [User] where U_username = '" + obj.U_username + "' and U_password = '" + obj.U_password + "'";
                List<User> adminDetails = con.Query<User>(query).ToList();
                if (adminDetails.Count != 0)
                {
                    FormsAuthentication.SetAuthCookie(obj.U_username, false);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username");
                    return RedirectToAction("Login", "Authentication");
                }
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


    }

}
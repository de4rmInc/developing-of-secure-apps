using Laba1_sql_injection.Models;
using Laba1_sql_injection.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laba1_sql_injection.Controllers
{
    public class LoginController : Controller
    {
        private UserContext db = new UserContext();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usr = db.Users.FirstOrDefault(user => user.UserName == model.UserName || user.Email == model.UserName);

                if (usr == null || !PasswordHash.Equals(usr.Password, model.Password))
                {
                    ModelState.AddModelError("", "Username or password is incorrect.");
                }
                else
                {
                    return RedirectToAction("Index", "Users");
                }
            }

            return View(model);
        }

        private bool IsValidLogin(UserViewModel model)
        {
            using (var connection = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                SqlCommand s = new SqlCommand("", connection);
                SqlParameter p = new SqlParameter();


            }

            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
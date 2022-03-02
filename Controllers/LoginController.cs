using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class LoginController : Controller
    {

        db_studentEntities db = new db_studentEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(user objchk)
        {
            if(ModelState.IsValid)
            {
                using (db_studentEntities db = new db_studentEntities())
                {
                    var obj = db.users.Where(a => a.UserName.Equals(objchk.UserName) && a.Password.Equals(objchk.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["userid"] = obj.id.ToString();
                        Session["username"] = obj.UserName.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The username or password is incorrect");
                    }
                }
            }

            
            return View(objchk);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
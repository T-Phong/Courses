using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Courses.Models;
namespace Courses.Controllers
{
    public class LoginController : Controller
    {
        SourceDbContext db = new SourceDbContext();
        //POST : login
        public JsonResult CheckLogin(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var check = from s in db.User
                        where s.IDUser == id
                        select s;
            User u = check.FirstOrDefault();
            if(u == null)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
            else
            {
                this.Session["usersession"] = u;
                return Json(u, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
    }
}
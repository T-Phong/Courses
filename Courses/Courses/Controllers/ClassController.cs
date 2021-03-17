using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Courses.Models;

namespace Courses.Controllers
{
    public class ClassController : Controller
    {
        public JsonResult test()
        {

            return Json(JsonRequestBehavior.AllowGet);
        }
        SourceDbContext db = new SourceDbContext();
        //--------------Json result-------------------
        // Get: Class
        public JsonResult GetClassByID(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var obj = from C in db.Class
                      where C.IDClass == id
                      select C;
            return Json(obj.SingleOrDefault(), JsonRequestBehavior.AllowGet);
        }

        //POST: Edit Class
        public JsonResult EditClass(Class c)
        {
            var query = from s in db.Class
                        where s.IDClass == c.IDClass
                        select s;
            foreach (Class obj in query)
            {
                obj.IDClass = c.IDClass;
                obj.ClassName = c.ClassName;
            }
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        //POST: Del class
        [HttpPost]
        public JsonResult DelClass(string idclass)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Class obj = db.Class.Find(idclass);
            db.Class.Remove(obj);
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        //POST: Add class
        public JsonResult AddClass(Class c)
        {
            db.Class.Add(c);
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        //--------------Action result-----------------
        // GET: ClassSubject
        public ActionResult Index()
        {
            return View(db.Class.ToList());
        }
        // Get: View add
        public ActionResult Add()
        {
            return View();
        }
        // Get: View edit
        public ActionResult Edit(string id)
        {
           
            return View();
        }
    }
}
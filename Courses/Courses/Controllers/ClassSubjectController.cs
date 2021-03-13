using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Courses.Models;
namespace Courses.Controllers
{
    public class ClassSubjectController : Controller
    {
        SourceDbContext db = new SourceDbContext();

        //----------------------------Json Result -----------------------------
        // Get: Subject
        public JsonResult GetSubjectByID(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var obj = from C in db.Subject
                      where C.IDSubject == id
                      select C;
            return Json(obj.SingleOrDefault(), JsonRequestBehavior.AllowGet);
        }
        //POST: Del Subject
        [HttpPost]
        public JsonResult DelSubject(string idsubject)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ClassSubject obj = db.Subject.Find(idsubject);
            db.Subject.Remove(obj);
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        //POST: Add Subject
        public JsonResult AddSubject(ClassSubject cs)
        {
            db.Subject.Add(cs);
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllClass()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var c = from C in db.Class
                    select C;
            return Json(c.ToList(), JsonRequestBehavior.AllowGet);
        }


        //----------------------------Action Result-----------------------------
        // GET: ClassSubject
        public ActionResult Index()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cs = from s in db.Subject
                              select s;
            return View(cs.ToList());
        }
    }
}
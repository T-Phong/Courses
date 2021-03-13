using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Courses.Models;
namespace Courses.Controllers
{
    public class UserController : Controller
    {
        SourceDbContext db = new SourceDbContext();

        //----------------------------Json--------------------------
        //Json GetAll
        [HttpGet]
        public JsonResult GetAll()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var person1 = from x in db.User
                          select x;

            //DateTime? dt = person1.FirstOrDefault();

            //string dt1 = dt?.ToString("yyyy-MM-dd");
            return Json(person1, JsonRequestBehavior.AllowGet);


        }
        //Get: Add
        [HttpGet]
        public JsonResult GetClassName()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var classname = from c in db.Class
                            select c;
            return Json(db.Class.ToList(), JsonRequestBehavior.AllowGet);
        }
        //POST: Add
        [HttpPost]
        public JsonResult AddUser(User u)
        {
            db.User.Add(u);
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        //POST: Edit
        public JsonResult EditUser(User u)
        {
            var query = from s in db.User
                        where s.IDUser == u.IDUser
                        select s;
            foreach(User user in query)
            {
                user.IDUser = u.IDUser;
                user.Name = u.Name;
                user.DOB = u.DOB;
                user.TypeUser = u.TypeUser;
                user.IDClass = u.IDClass;
            }
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }

        //POST: Del
        public JsonResult DelUser(string u)
        {
            var user = db.User.Find(u);
            db.User.Remove(user);
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        // --------------------------------------Actionresult---------------------------
        // GET: User
        public ActionResult Index()
        {
            var obj = GetAll();
            return View(obj);
        }
        // Get: View add
        public ActionResult Add()
        {
            var obj = GetClassName();
            return View(obj);
        }
        // Get: View edit
        public ActionResult Edit(string id)
        {
            dynamic mymodel = new ExpandoObject();
            db.Configuration.ProxyCreationEnabled = false;
            var u = from p in db.User
                    join c in db.Class on p.IDClass equals c.IDClass
                    where p.IDUser == id
                    select new UserClass
                    {
                        _IDUser = p.IDUser,
                        _Name = p.Name,
                        _DOB = p.DOB,
                        _TypeUser = p.TypeUser,
                        _IDClass = p.IDClass,
                        _ClassName = c.ClassName
                    };
            UserClass uc = u.SingleOrDefault();
            var cs = from c in db.Class
                     select c;
            mymodel.ClassSubject = cs;
            mymodel.User = u;
            //var u = db.User.Find(id);
            return View(mymodel);
        }
    }
}
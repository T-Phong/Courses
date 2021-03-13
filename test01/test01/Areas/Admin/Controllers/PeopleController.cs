using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace test01.Areas.Admin.Controllers
{
    public class PeopleController : Controller
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();
        // GET: Admin/People/Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: Admin/People/Login
        //To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(string FirstName, string LastName)
        {
            if (ModelState.IsValid)
            {
                var data = db.Persons.Where(s => s.FirstName.Equals(FirstName) && s.LastName.Equals(LastName)).ToList();
                if (data.Count() > 0)
                {
                    var user1 = new Person
                    {
                        FirstName = (from user in data
                                     select user.FirstName).First().ToString(),
                        LastName = (from user in data
                                    select user.LastName).First().ToString()
                    };
                    this.Session["user1"] = user1;
                    return RedirectToAction("Index");

                }
                //db.Persons.Add(person);
                //db.SaveChanges();
                //return RedirectToAction("Index");

                else
                {
                    TempData["error"] = "Login Failed";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ViewBag.error = "Login failed";
                return RedirectToAction("Login");
            }
        }



        //GET ALL
        [HttpGet]
        public JsonResult GetALL()
        { 
            var person = from cust in db.Persons
                         select cust;
            return Json(person.ToList(), JsonRequestBehavior.AllowGet);
        }

        // SORT


        //sort by age
        [HttpGet]
        public JsonResult SortByAge()
        {
            var person  = from cust in db.Persons
                          orderby cust.ID descending
                          select cust;

            return Json(person.ToList(), JsonRequestBehavior.AllowGet) ;
        }
        //add
        [HttpPost]
        public JsonResult AddLinq(Person adding)
        {
            
            db.Persons.Add(adding);
            
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        //delete
        [HttpPost]
        public JsonResult DeleteLinq(int id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
            return Json(db.SaveChanges(),JsonRequestBehavior.AllowGet);

        }
        //view add
        public ActionResult AddLinqView()
        {

            return View();
        }
        //view edit
        [HttpGet]
        public ActionResult EditView(int id)
        {
            Person obj = db.Persons.Find(id);
            return View(obj);
        }


        [HttpGet]
        public JsonResult FindByAge(int? Age)
        {
            var obj = from p in db.Persons
                      where p.Age == Age
                      select p;
            return Json(obj.ToList(), JsonRequestBehavior.AllowGet);
        }



        //Edit action
        [HttpPost]
        public JsonResult EditLinq(Person edit)
        {

            //var obj = db.Persons.Single(x => x.ID == edit.ID && x.FirstName == edit.FirstName && x.LastName == edit.LastName
            //&& x.Age == edit.Age);
            var query = from p in db.Persons
                        where p.ID == edit.ID
                        select p;
            foreach (Person e in query)
            {
                e.ID = edit.ID;
                e.LastName = edit.LastName;
                e.FirstName = edit.FirstName;
                e.Age = edit.Age;
            }
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        //Get: Find by Age
        













        // GET: Admin/People
        public ActionResult Index()
        {
            return View(db.Persons.ToList());
        }

        // GET: Admin/People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: Admin/People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstName,Age")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: Admin/People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Admin/People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstName,Age")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: Admin/People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Admin/People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
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

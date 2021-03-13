using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Courses.Models;
namespace Courses.Controllers
{
    public class ExerciseController : Controller
    {
        SourceDbContext db = new SourceDbContext();


        //---------------------------------- Json Result ---------------------
        //POST Add
        public JsonResult AddEx(ExerciseSubject ex)
        {
            db.Exercise.Add(ex);
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }

        //POST del
        [HttpPost]
        public JsonResult DelEx(string iduser,string idex)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var obj = from ex in db.Exercise
                        where ex.IDUser == iduser && ex.IDExercise == idex
                        select ex;
            ExerciseSubject csdel = obj.SingleOrDefault();
            db.Exercise.Remove(csdel);
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        //POST Save Edit
        public JsonResult SaveEdit(ExerciseSubject ex)
        {
            var query = from s in db.Exercise
                        where s.IDUser == ex.IDUser && s.IDExercise == ex.IDExercise && s.IDSubject == ex.IDSubject
                        select s;
            foreach (ExerciseSubject e in query)
            {
                e.IDExercise = ex.IDExercise;
                e.IDUser = ex.IDUser;
                e.ExerciseName = ex.ExerciseName;
                e.Content = ex.Content;
                e.CreateDate = ex.CreateDate;
                e.DeadLine = ex.DeadLine;
                e.IDSubject = ex.IDSubject;
                e.Mark = ex.Mark;
            }
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }



        //---------------------------------- Action Result---------------------
        // GET: Exercise
        public ActionResult Index()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var ex = from e in db.Exercise
                     select e;
            return View(ex.ToList());
        }
        public ActionResult Add()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var sub = from e in db.Subject
                     select e;
            return View(sub.ToList());
        }
        public ActionResult Details(string iduser,string idex,string idcs)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var detailsEx = from ex in db.Exercise
                            join cs in db.Subject
                            on ex.IDSubject equals cs.IDSubject
                            join s in db.User
                            on ex.IDUser equals s.IDUser
                            where ex.IDUser == iduser && ex.IDExercise == idex && cs.IDSubject == idcs
                            select new DetailsExercise
                            {
                                IDExercise = ex.IDExercise,
                                IDUser = ex.IDUser,
                                Name = s.Name,
                                ExerciseName = ex.ExerciseName,
                                Content = ex.Content,
                                CreateDate = ex.CreateDate,
                                DeadLine = ex.DeadLine,
                                IDSubject = ex.IDSubject,
                                SubjectName = cs.SubjectName,
                                Mark = ex.Mark
                            };
            DetailsExercise obj = detailsEx.SingleOrDefault();
            return View(obj);
        }
    }
}
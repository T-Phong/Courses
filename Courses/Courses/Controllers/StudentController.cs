using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Courses.Models;
using Microsoft.Ajax.Utilities;

namespace Courses.Controllers
{
    public class StudentController : Controller
    {
        SourceDbContext db = new SourceDbContext();
        //calculate mark
        public JsonResult Mark(string idex, string iduser, string idsub, double mark)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var query1 = db.Exercise.Single(e => e.IDExercise == idex && e.IDUser == iduser && e.IDSubject == idsub);
            query1.Mark = mark;
            db.SaveChanges();
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        //Teach Add Exercise
        public JsonResult AddEx(string idex, string exname, string createdate, string deadline, string idsub)
        {
            DateTime? crdate = string.IsNullOrEmpty(createdate) ? (DateTime?)null : DateTime.Parse(createdate);
            DateTime? dl = string.IsNullOrEmpty(deadline) ? (DateTime?)null : DateTime.Parse(deadline);
            db.Configuration.ProxyCreationEnabled = false;
            var query = from e in db.Exercise
                        where e.IDSubject == idsub
                        select e.IDUser;
            List<string> stuinsub = query.Distinct().ToList();
           
            foreach (var item in stuinsub)
            {
                ExerciseSubject add = new ExerciseSubject()
                {
                    IDExercise = idex,
                    IDUser = item,
                    ExerciseName = exname,
                    CreateDate = crdate,
                    DeadLine = dl,
                    IDSubject = idsub
                };
                db.Exercise.Add(add);
                db.SaveChanges();
            }
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        //POST student submit exercise
        public JsonResult SaveEx(string idex, string iduser, string content, string idsubject)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var query1 = db.Exercise.Single(e => e.IDExercise == idex && e.IDUser == iduser && e.IDSubject == idsubject);
            query1.Content = content;
            db.SaveChanges();
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet); ;
        }
        //Get name teacher of subject
        public JsonResult GetNameTeachOfSubject(string idsubject)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cs = from s in db.Subject
                     join u in db.User
                     on s.IDClass equals u.IDClass
                     where u.TypeUser == 2 && s.IDSubject == idsubject
                     select new TeacherSubject
                     {
                         IDUser = u.IDUser,
                         Name = u.Name,
                         IDSubject = s.IDSubject,
                         SubjectName = s.SubjectName,
                         IDClass = s.IDClass
                     };
            return Json(cs.ToList(), JsonRequestBehavior.AllowGet);
        }
        public void GetNameTeachOfSubject1(string idsubject)
        {
            var teachname = from u in db.User
                            join s in db.Subject on u.IDClass equals s.IDClass
                            where u.TypeUser == 2 && s.IDSubject == idsubject
                            select u.Name;

            ViewBag.Teachname1 = teachname.ToList();
        }
        //count student of subject
        public int StudentofSub(string idsubject)
        {
            int num = 0;
            db.Configuration.ProxyCreationEnabled = false;
            var cs = from s in db.Subject
                     join u in db.User
                     on s.IDClass equals u.IDClass
                     where u.TypeUser == 1 && s.IDSubject == idsubject
                     select u.Name;
            num = cs.Count();
            return num;
        }
        // name of teach in subject
        public string Showname(string idsubject)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cs = from s in db.Subject
                     join u in db.User
                     on s.IDClass equals u.IDClass
                     where u.TypeUser == 2 && s.IDSubject == idsubject
                     select u.Name;
            var ts = new List<string>();
            foreach (string item in cs)
            {
                ts.Add(item);
            };
            if(ts.Count()>0)
            {
                foreach (var item in ts)
                {
                    return item;
                }
            }
            
            return "";
        }
        //count date
        public static int HowManyDaysFromToday(DateTime appointment)
        {
            var today = DateTime.Today; //like DateTime.Now but with no time aspect
            var appDay = appointment.Date;
            return (int)appDay.Subtract(today).TotalDays;
        }
        // GET: Subject detail
        public ActionResult SubjectDetail(string idsub)
        {
            var sessionuser = Session["usersession"] as Courses.Models.User;
            db.Configuration.ProxyCreationEnabled = false;
            //get subject name
            var subname = from s in db.Subject
                          where s.IDSubject == idsub
                          select s.SubjectName;
            ViewBag.SubjectName = subname.SingleOrDefault();
            //get teach name of subject
            var teachname = from u in db.User
                            join s in db.Subject on u.IDClass equals s.IDClass
                            where u.TypeUser == 2 && s.IDSubject == idsub
                            select u.Name;
            ViewBag.Teachname = teachname.ToList();
            //count student of subject
            var countstudentdetail = from u in db.User
                                     join s in db.Subject on u.IDClass equals s.IDClass
                                     where u.TypeUser == 1 && s.IDSubject == idsub
                                     select u.Name;
            ViewBag.Countstudentdetail = countstudentdetail.Count();
            //get exercise name of student
            var exname = from ex in db.Exercise
                         join s in db.Subject on ex.IDSubject equals s.IDSubject
                         join u in db.User on ex.IDUser equals u.IDUser
                         where s.IDSubject == idsub && ex.IDUser == sessionuser.IDUser
                         select ex;
            ViewBag.Exname = exname.ToList();
            //get exercise name of teach
            var listexofteach = from e in db.Exercise
                                join s in db.Subject on e.IDSubject equals s.IDSubject
                                join u in db.User on s.IDClass equals u.IDClass
                                where e.IDSubject == idsub && u.IDUser == sessionuser.IDUser
                                select new MiniExercise
                                {
                                    IDExercise = e.IDExercise,
                                    ExerciseName = e.ExerciseName,
                                    IDSubject = e.IDSubject
                                };
            var result = listexofteach.DistinctBy(i => i.IDExercise).ToList();
            List<MiniExercise> mini = result;
            ViewBag.ExnameTeach = mini;
            return View();
        }
        // GET: Student
        public ActionResult Index()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var getcountstudent = from s in db.User
                                  where s.TypeUser == 1
                                  select s;
            int countstudent = getcountstudent.Count();
            var getcountsubject = from s in db.Subject
                                  select s;
            int countsubject = getcountsubject.Count();
            var getcountteach = from t in db.User
                                where t.TypeUser == 2
                                select t;
            int countteach = getcountteach.Count();

            ViewBag.CountStudents = countstudent;
            ViewBag.CountSubject = countsubject;
            ViewBag.CountTeach = countteach;

            var cs = from s in db.Subject
                     select s;
            //join u in db.User
            //on s.IDClass equals u.IDClass
            //where u.TypeUser == 2
            //select new TeacherSubject
            //{
            //    IDUser = u.IDUser,
            //    Name = u.Name,
            //    IDSubject = s.IDSubject,
            //    SubjectName = s.SubjectName,
            //    IDClass = s.IDClass
            //};
            //var temp = from s in db.Subject
            //           select s.IDSubject;
            //foreach (var item in temp)
            //{
            //    var teachname = from u in db.User
            //                    join s in db.Subject on u.IDClass equals s.IDClass
            //                    where u.TypeUser == 2 && s.IDSubject == item
            //                    select u.Name;
            //    ViewBag.Teachname1 = teachname.ToList();
            //}                   
            return View(cs.ToList());
        }
        public ActionResult ExerciseDetail(string subject, string exercise)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var sessionuser = Session["usersession"] as Courses.Models.User;
            //get exercise detail 
            var exdetail = from ex in db.Exercise
                           where ex.IDSubject == subject && ex.IDExercise == exercise && ex.IDUser == sessionuser.IDUser
                           select ex;
            ViewBag.Exdetail = exdetail.SingleOrDefault();

            var subname = from s in db.Subject
                          where s.IDSubject == subject
                          select s.SubjectName;
            ViewBag.SubjectName = subname.SingleOrDefault();

            var dayleft = from ex in db.Exercise
                           where ex.IDSubject == subject && ex.IDExercise == exercise && ex.IDUser == sessionuser.IDUser
                          select ex.DeadLine;
            DateTime? dt = dayleft.SingleOrDefault();
            DateTime dtValue = (DateTime)dt;
            int b = HowManyDaysFromToday(dtValue);
            ViewBag.Dayremain = b;
            return View();
        }
        public ActionResult CalculatedGrades(string idex, string idsubject)
        {
            var sessionuser = Session["usersession"] as Courses.Models.User;
            db.Configuration.ProxyCreationEnabled = false;
            var listex = from ex in db.Exercise
                         where ex.IDExercise == idex && ex.IDSubject == idsubject
                         select ex;
            List<ExerciseSubject> list = listex.ToList();
            ViewBag.ListExercise = list;

            var subname = from s in db.Subject
                          where s.IDSubject == idsubject
                          select s.SubjectName;
            ViewBag.SubjectName = subname.SingleOrDefault();

            var dayleft = from ex in db.Exercise
                          where ex.IDSubject == idsubject && ex.IDExercise == idex
                          select ex;
            var result = dayleft.DistinctBy(i => i.DeadLine);
            DateTime? dt = result.Select(x => x.DeadLine).SingleOrDefault();
            DateTime dtValue = (DateTime)dt;
            int b = HowManyDaysFromToday(dtValue);
            ViewBag.Dayremain = b;

            return View();
        }
    }
}
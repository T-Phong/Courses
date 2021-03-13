using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courses.Models
{
    public class DetailsExercise
    {
        public string IDExercise { get; set; }
        public string IDUser { get; set; }
        public string Name { get; set; }
        public string ExerciseName { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> DeadLine { get; set; }
        public string IDSubject { get; set; }
        public string SubjectName { get; set; }
        public Nullable<double> Mark { get; set; }
    }
}
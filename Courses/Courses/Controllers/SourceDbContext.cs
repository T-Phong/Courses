using Courses.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace Courses.Controllers
{
    public class SourceDbContext : DbContext
    {
        public SourceDbContext()
           : base("name=CourseEntities")
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<ClassSubject> Subject { get; set; }
        public DbSet<ExerciseSubject> Exercise { get; set; }
        




    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolProject.Models
{
    public class TeacherViewModel
    {
        public Teacher Teacher { get; set; }
        public List<Course> CoursesTaught { get; set; }
    }

}
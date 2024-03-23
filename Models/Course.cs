using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolProject.Models
{
    public class Course
    {
        public int classid { get; set; }
        public string classcode { get; set; }
        public int teacherid { get; set; }

        public string classname { get; set; }
    }
}
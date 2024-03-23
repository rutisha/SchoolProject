using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    public class ClassController : Controller
    {

        // GET: Class/List -> return a list of class and classcode
        public ActionResult List()
        {
            ClassesDataController controller = new ClassesDataController();
            IEnumerable<Course> Courses = controller.ListClasses();

            return View(Courses);
        }
    }
}
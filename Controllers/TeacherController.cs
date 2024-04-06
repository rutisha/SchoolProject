using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        
        // GET: Teacher/List/{Searchkey} -> return a list of teachers by searching the keyword
        public ActionResult List(string Searchkey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(Searchkey);

            return View(Teachers);
        }

        // GET: Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeachers = controller.Teachersinfo(id);
           

            return View(NewTeachers);
        }
       
         public ActionResult New()
        {
            return View();

        }

        //POST : /Teacher/Create
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime?HireDate, decimal? Salary)
        {
            if (string.IsNullOrEmpty(TeacherFname) || string.IsNullOrEmpty(TeacherLname) || string.IsNullOrEmpty(EmployeeNumber) || HireDate == null || Salary == null)
            {
                ViewBag.Error = "All fields are required.";
                return View("New");
            }
            //Identify that this method is running
            //Identify the inputs provided from the form

            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherfname = TeacherFname;
            NewTeacher.teacherlname = TeacherLname;
            NewTeacher.employeenum = EmployeeNumber;
            NewTeacher.hiredate = (DateTime)HireDate;
            NewTeacher.salary = Salary ?? 0;


            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        //GET: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.Teachersinfo(id);


            return View(NewTeacher);
        }
        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

    }
}
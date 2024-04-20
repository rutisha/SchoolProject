using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
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
        //GET: /Teacher/Update/{id}
        public ActionResult Update(int id)
        {
            //Need to get the information about the teacher
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.Teachersinfo(id);

            return View(SelectedTeacher);
        }

        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system,
        /// with new values. Conveys this information to the API, and redirects to the "Teacher Show"
        /// page of our updated teacher.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TeacherFname"></param>
        /// <param name="TeacherLname"></param>
        /// <param name="HireDate"></param>
        /// <param name="Salary"></param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : Teacher/Update/10
        /// {
        /// "TeacherFname": "Rutisha";
        /// "TeacherLname":   "Patel";
        /// "HireDate": "8/01/2024";
        /// "Salary": "40";
        /// }
        /// </example>
        // POST: /Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime?HireDate, decimal?Salary)
        {
            TeacherDataController controller = new TeacherDataController();
            if (string.IsNullOrEmpty(TeacherFname) || string.IsNullOrEmpty(TeacherLname) || string.IsNullOrEmpty(EmployeeNumber) || !HireDate.HasValue || !Salary.HasValue)
            {
                ViewBag.Error = "All fields are required.";
                Teacher SelectedTeacher = controller.Teachersinfo(id);
                return View("Update", SelectedTeacher);

            }

            Teacher UpdatedTeacher = new Teacher();
            UpdatedTeacher.teacherfname = TeacherFname;
            UpdatedTeacher.teacherlname = TeacherLname;
            UpdatedTeacher.employeenum = EmployeeNumber;
            UpdatedTeacher.hiredate = (DateTime)HireDate;
            UpdatedTeacher.salary = Salary ?? 0;

          
            controller.UpdateTeacher(id, UpdatedTeacher);

            return RedirectToAction("Show/" + id);

        }

        //GET : /Teacher/Ajax_Update
        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.Teachersinfo(id);

            return View(SelectedTeacher);
        }

    }
}
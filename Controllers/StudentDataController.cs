using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    public class StudentDataController : ApiController
    {
        //The database class which allow us to access our MySql Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the student table of school database.
        /// <summary>
        /// Returns a list of student with student ID in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListStudents</example>
        /// <returns>
        /// A list of students (first names and last names) with student ID
        /// </returns>
        [HttpGet]
        [Route("api/studentdata/liststudents")]
        public List<Student> ListStudents()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Create a new command (query) for our database
            MySqlCommand Cmd = Conn.CreateCommand();

            //SQL QUERY
            Cmd.CommandText = "Select * from students";

            //Gather Results of Query into a variable
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            List<Student> StudentNames = new List<Student>();

            //Loop through Each Row
            while (ResultSet.Read())
            {
                //Access Column information by the column name as an index
                int Studentid = ResultSet.GetInt32("studentid");
                string Studentfname = ResultSet["studentfname"].ToString();
                string Studentlname = ResultSet["studentlname"].ToString();
                string StudentID = ResultSet["studentnumber"].ToString();

                //Add to the Teachers List
                Student StudentName = new Student();
                StudentName.studentid = Studentid;
                StudentName.studentfname = Studentfname;
                StudentName.studentlname = Studentlname;
                StudentName.studentnumber = StudentID;


                StudentNames.Add(StudentName);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the list of teacher names
            return StudentNames;
        }
    }
}

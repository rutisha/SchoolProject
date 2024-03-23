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
    public class TeacherDataController : ApiController
    {
        //The database class which allow us to access our MySql Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teacher table of school database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/teacherdata/listteachers/{Searchkey}")]
        public List<Teacher> ListTeachers(string Searchkey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Create a new command (query) for our database
            MySqlCommand Cmd = Conn.CreateCommand();

            //SQL QUERY
            Cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower('%"+Searchkey+"%') or lower(teacherlname) like lower('%"+Searchkey+"%')";

            //Gather Results of Query into a variable
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            List<Teacher> TeachersNames = new List<Teacher>();

            //Loop through Each Row
            while(ResultSet.Read())
            {
                //Access Column information by the column name as an index
                int Teacherid = ResultSet.GetInt32("teacherid");
                string Teacherfname = ResultSet["teacherfname"].ToString();
                string Teacherlname = ResultSet["teacherlname"].ToString();

                //Add to the Teachers List
                Teacher TeacherName = new Teacher();
                TeacherName.teacherid = Teacherid;
                TeacherName.teacherfname = Teacherfname;
                TeacherName.teacherlname = Teacherlname;


                TeachersNames.Add(TeacherName);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the list of teacher names
            return TeachersNames;
        }

        public Teacher Teachersinfo(int id)
        {
            Teacher Teacherinfo = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Create a new command (query) for our database
            MySqlCommand Cmd = Conn.CreateCommand();

            //SQL QUERY
            Cmd.CommandText = "Select * from teachers where teacherid = " + id;

            //Gather Results of Query into a variable
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            

            //Loop through Each Row
            while (ResultSet.Read())
            {
                //Access Column information by the column name as an index
                int Teacherid = ResultSet.GetInt32("teacherid");
                string Teacherfname = ResultSet["teacherfname"].ToString();
                string Teacherlname = ResultSet["teacherlname"].ToString();
                string EmployeeNum = ResultSet["employeenumber"].ToString() ;
                decimal Salary = ResultSet.GetDecimal("salary");

                //Add to the Teachers List
               
                Teacherinfo.teacherid = Teacherid;
                Teacherinfo.teacherfname = Teacherfname;
                Teacherinfo.teacherlname = Teacherlname;
                Teacherinfo.employeenum = EmployeeNum;
                Teacherinfo.salary = Salary;

            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the list of teacher names
            return Teacherinfo;
        }
    }
}

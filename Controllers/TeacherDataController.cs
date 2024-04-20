using System;
using System.Collections.Generic;
using System.Web.Http;
using MySql.Data.MySqlClient;
using SchoolProject.Models;
using System.Web.Http.Cors;
using System.Diagnostics;

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
            Cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower('%" + Searchkey + "%') or lower(teacherlname) like lower('%" + Searchkey + "%')";

            //Gather Results of Query into a variable
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            List<Teacher> TeachersNames = new List<Teacher>();

            //Loop through Each Row
            while (ResultSet.Read())
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
                string EmployeeNum = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = ResultSet.GetDecimal("salary");

                //Add to the Teachers List

                Teacherinfo.teacherid = Teacherid;
                Teacherinfo.teacherfname = Teacherfname;
                Teacherinfo.teacherlname = Teacherlname;
                Teacherinfo.employeenum = EmployeeNum;
                Teacherinfo.hiredate = HireDate;
                Teacherinfo.salary = Salary;

            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the list of teacher names
            return Teacherinfo;
        }

        /// <summary>
        /// This method will add a new teacher data in the database
        /// </summary>
        /// <param name="NewTeacher">Teacher Object</param>
        /// <returns></returns>
        /// <example>
        /// api/TeacherData/AddTeacher
        /// POST DATA: Teacher Object
        /// </example>

        [HttpPost]
        [EnableCors(origins:"*",methods:"*",headers:"*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            MySqlConnection Conn = School.AccessDatabase();


            Conn.Open();

            MySqlCommand Cmd = Conn.CreateCommand();

            string query = "Insert into teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherId, @TeacherFname, @TeacherLname, @EmployeeNumber, @HireDate, @Salary)";

            Cmd.CommandText = query;

            Cmd.Parameters.AddWithValue("@TeacherId", NewTeacher.teacherid);
            Cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.teacherfname);
            Cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.teacherlname);
            Cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.employeenum);
            Cmd.Parameters.AddWithValue("@HireDate", NewTeacher.hiredate);
            Cmd.Parameters.AddWithValue("@Salary", NewTeacher.salary);


            Cmd.Prepare();
            Cmd.ExecuteNonQuery();

            Conn.Close();

        }

        /// <summary>
        /// This method will delete a teacher data in the database
        /// </summary>
        /// <param name="TeacherId">The teacher id primary key</param>
        /// <example>
        /// POST: api/teacherdata/deleteteacher/2
        /// </example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            string query = "DELETE from teachers where teacherid=@id";
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@id", id);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            Conn.Close();

        }
        /// <summary>
        /// Updates a teacher on the MySQL database. 
        /// </summary>
        /// <param name="TeacherId">The id of the teacher in the system</param>
        /// <param name="UpdateTeacher">post content, all teacher attributes</param>
        /// <example>POST : /api/TeacherData/UpdateTeacher/7
        /// {"teacherfname":"Christine", "teacherlname":"Bittle", "hiredate":"","salary":"6000"}
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/UpdateTeacher/{TeacherId}")]
        public void UpdateTeacher(int TeacherId, [FromBody] Teacher UpdatedTeacher)
        { 
            string query = "UPDATE teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber = @EmployeeNum, hiredate=@HireDate, salary=@Salary WHERE teacherid=@TeacherId";

            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and data base
            Conn.Open();


            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = query;
            //SQL Query

            cmd.Parameters.AddWithValue("@TeacherId", TeacherId);
            cmd.Parameters.AddWithValue("@TeacherFname", UpdatedTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@TeacherLname", UpdatedTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@EmployeeNum", UpdatedTeacher.employeenum);
            cmd.Parameters.AddWithValue("@HireDate", UpdatedTeacher.hiredate);
            cmd.Parameters.AddWithValue("@Salary", UpdatedTeacher.salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

    }
}

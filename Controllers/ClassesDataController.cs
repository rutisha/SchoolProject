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
    public class ClassesDataController : ApiController
    {
        //The database class which allow us to access our MySql Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the classes table of school database.
        /// <summary>
        /// Returns a list of Classname with classcode  in the system
        /// </summary>
        /// <example>GET api/ClassesData/ListClasses</example>
        /// <returns>
        /// A list of classes name with classcode
        /// </returns>
        [HttpGet]
        [Route("api/classesdata/listclasses")]
        public List<Course> ListClasses()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Create a new command (query) for our database
            MySqlCommand Cmd = Conn.CreateCommand();

            //SQL QUERY
            Cmd.CommandText = "Select * from classes";

            //Gather Results of Query into a variable
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            List<Course> ClassNames = new List<Course>();

            //Loop through Each Row
            while (ResultSet.Read())
            {
                //Access Column information by the column name as an index
                int Classid = ResultSet.GetInt32("classid");
                string Classname = ResultSet["classname"].ToString();
                string Classcode = ResultSet["classcode"].ToString();

                //Add to the Teachers List
                Course CourseName = new Course();
                CourseName.classid = Classid;
                CourseName.classname = Classname;
                CourseName.classcode = Classcode;


                ClassNames.Add(CourseName);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the list of teacher names
            return ClassNames;
        }
    }
}

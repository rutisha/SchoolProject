﻿
function AddTeacher() {


	var URL = "https://localhost:44320/api/teacherdata/Addteacher";

	var rq = new XMLHttpRequest();

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var Salary = document.getElementById('Salary').value;



	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"EmployeeNumber": EmployeeNumber,
		"Salary": Salary
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));


}



	function UpdateTeacher(TeacherId) {

		var URL = "https://localhost:44320/api/teacherdata/UpdatTeacher/" + TeacherId;

		var rq = new XMLHttpRequest();
		// where is this request sent to?
		// is the method GET or POST?
		// what should we do with the response?

		var TeacherFname = document.getElementById('TeacherFname').value;
		var TeacherLname = document.getElementById('TeacherLname').value;
		var EmployeeNumber = document.getElementById('EmployeeNumber').value;
		var Salary = document.getElementById('Salary').value;



		var TeacherData = {
			"TeacherFname": TeacherFname,
			"TeacherLname": TeacherLname,
			"EmployeeNumber": EmployeeNumber,
			"Salary": Salary
		};


		rq.open("POST", URL, true);
		rq.setRequestHeader("Content-Type", "application/json");
		rq.onreadystatechange = function () {
			//ready state should be 4 AND status should be 200
			if (rq.readyState == 4 && rq.status == 200) {
				//request is successful and the request is finished

				//nothing to render, the method returns nothing.


			}

		}
		//POST information sent through the .send() method
		rq.send(JSON.stringify(TeacherData));

}

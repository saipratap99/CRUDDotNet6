// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const baseURL = "https://dotnetcore6.azurewebsites.net";
//const baseURL = "http://localhost:5193";
let students;
$(document).ready(function () {

    // jQuery methods go here...

    
});


$.ajax({
    type: "GET",
    url: `${baseURL}/api/Student`,
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: OnSuccess,
    failure: function (response) {
        alert(response.d);
    }
});

function OnSuccess(response) {
    students = response;
    console.log(JSON.stringify(response))
    for (let student of students) {
        console.log(student);
        let row = `<tr><td>${student.id}</td><td>${student.name}</td><td>${student.email}</td><td>${student.city}</td></tr>`;
        $("table#studentsTable tbody").append(row);
    }
}
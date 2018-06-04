/* Definition of global variables */ 
var showHideAddCourse = $("#add-course-form-container");
var showHideRemoveCourse = $("#remove-course-form-container");
var showHideAddStudent = $("#add-student-form-container");
var showHideRemoveStudent = $("#remove-student-form-container");

/* Definition of initial operation on the html pages */
$(document).ready(function () {
    showHideAddCourse.hide();
    showHideRemoveCourse.hide();
    showHideAddStudent.hide();
    showHideRemoveStudent.hide();
});

/* Assigning to the button its function */
$("#show-add-course-form").on("click", function () {
    showHideRemoveCourse.hide();
    showHideAddCourse.toggle(500);
});

$("#show-remove-course-form").on("click", function () {
    showHideAddCourse.hide();
    showHideRemoveCourse.toggle(500);
});

$("#add-course").on("click", function () {
    AddCourseHandler();
});

$("#remove-course").on("click", function () {
    RemoveCourseHandler();
});

$("#show-add-student-form").on("click", function () {
    showHideRemoveStudent.hide();
    showHideAddStudent.toggle(500);
});

$("#show-remove-student-form").on("click", function () {
    showHideAddStudent.hide();
    showHideRemoveStudent.toggle(500);
});

$("#add-student").on("click", function () {
    AddStudentHandler();
});

$("#remove-student").on("click", function () {
    RemoveStudentHandler();
});

/* Functions */
function AddCourseHandler() {
    var name = $("#Name").val();
    var ects = parseInt(document.forms.addCourseForm.NumberOfECTS.value);
    var lect = parseInt(document.forms.addCourseForm.LecturesHours.value);
    var lab = parseInt(document.forms.addCourseForm.LaboratoriesHours.value);
    var dcpt = document.getElementById("Description").value;

    if (isNaN(lect)) lect = 0;
    if (isNaN(lab)) lab = 0;

    if (name.trim() === "") { alert("Type a valid name.") }
    else if (isNaN(ects)) { alert("Insert the number of ECTS.") }
    else {
        $.post("/Course/AddCourse", $.param({
            Name: name, NumberOfECTS: ects, LecturesHours: lect,
            LaboratoriesHours: lab, Description: dcpt }), 
            function (getResult) {
                if (getResult.success) { location.reload(true) }
                else { alert("This course already exists!") }
        });
    }
}

function RemoveCourseHandler() {
    var select = document.getElementById("removeCourseName");
    var name = select.options[select.selectedIndex].text;
    var ects = select.options[select.selectedIndex].value;

    $.post("/Course/RemoveCourse", $.param({ Name: name, NumberOfECTS: ects }),
           function () { location.reload(true); });
}

function AddStudentHandler() {
    var last = $("#LastName").val();
    var first = $("#FirstName").val();

    if (last.trim() === "") { alert("Type a valid last name.") }
    else if (first.trim() === "") { alert("Type a valid first name.") }
    else {
        $.post("/ManageStudent/AddStudent", $.param({ LastName: last, FirstName: first }),
            function (getResult) {
                if (getResult.success) { location.reload(true); }
                else { alert("You have already added this student!") }
            });
    }
}

function RemoveStudentHandler() {
    var select = document.getElementById("removeStudentName");
    var completeName = select.options[select.selectedIndex].text.split("-")[1];
    var last = completeName.split(" ")[1];
    var first = completeName.split(" ")[2];

    $.post("/ManageStudent/RemoveStudent", $.param({ LastName: last, FirstName: first }),
        function () { location.reload(true); });
}
/* Definition of global variables */ 
var showHideAddCourse = $("#add-course-form-container");
var showHideRemoveCourse = $("#remove-course-form-container");
var showHideAddStudent = $("#add-student-form-container");
var showHideRemoveStudent = $("#remove-student-form-container");
var showHideAddCS = $("#add-cs-form-container");
var showHideRemoveCS = $("#remove-cs-form-container");

var continueBtn = $("#continue");
var backBtn = $("#back-btn");
var removeAttendance = $("#remove-course-student");

/* Definition of initial operation on the html pages */
$(document).ready(function () {
    showHideAddCourse.hide();
    showHideRemoveCourse.hide();
    showHideAddStudent.hide();
    showHideRemoveStudent.hide();
    showHideAddCS.hide();
    showHideRemoveCS.hide();

    removeAttendance.hide();
    backBtn.hide();
});

/* Assigning to the button its function */
//Add-Remove Course
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


//Add-Remove Student
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

//Add-Remove CourseStudent
$("#show-add-cs-form").on("click", function () {
    showHideRemoveCS.hide();
    showHideAddCS.toggle(500);
});

$("#show-remove-cs-form").on("click", function () {
    showHideAddCS.hide();
    showHideRemoveCS.toggle(500);
});

$("#add-course-student").on("click", function () {
    AddCsHandler();
});

continueBtn.on("click", function () {
    removeAttendance.show();
    backBtn.show();
    $("#csIdRemove").prop('disabled', true);
    continueBtn.prop('disabled', true);

    var cId = parseInt(document.getElementById("csIdRemove").options[document.getElementById("csIdRemove").selectedIndex].text.split("-")[0].trim());
    $.get("/ManageStudent/StudentsByCourse", $.param({ CourseId: cId }), function (result)
    {
        $("#addHere").after('<div id="added" class="form-group col-md-10"><label id="optionsHere" for="csSnRemove">Select the student who has to be removed</label><div>');
        var options = '<select class="form-control" id="csSnRemove">';
        for (i = 0; i < result.length; i++) {
            var snF = "";
            if (result[i].serialNumber < 100) { snF += 0; }
            if (result[i].serialNumber < 10) { snF += 0; }
            snF += result[i].serialNumber;
            var completeName = result[i].lastName + " " + result[i].firstName;
            options += '<option>' + snF + ' - ' + completeName + '</option>';
        }
        options += '</select>';
        $("#optionsHere").after(options);
    });
});

backBtn.on("click", function () {
    removeAttendance.hide();
    backBtn.hide();
    $("#csIdRemove").prop('disabled', false);
    continueBtn.prop('disabled', false);
    $("#added").remove();
});

$("#remove-course-student").on("click", function () {
    RemoveCsHandler();
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

function AddCsHandler() {
    var courseId = parseInt(document.getElementById("csIdAdd").options[document.getElementById("csIdAdd").selectedIndex].text.split("-")[0].trim());             
    var serialNumber = parseInt(document.getElementById("csSnAdd").options[document.getElementById("csSnAdd").selectedIndex].text.split("-")[0].trim());

    $.post("/Course/AddCourseStudent", $.param({ CourseId: courseId, StudentSerialNumber: serialNumber }),
        function (getResult) {
            if (getResult.success) { location.reload(true); }
            else { alert("You have already added this student!") }
        });
}

function RemoveCsHandler() {
    var courseId = parseInt(document.getElementById("csIdRemove").options[document.getElementById("csIdRemove").selectedIndex].text.split("-")[0].trim());
    var serialNumber = parseInt(document.getElementById("csSnRemove").options[document.getElementById("csSnRemove").selectedIndex].text.split("-")[0].trim());

    $.post("/Course/RemoveCourseStudent", $.param({ CourseId: courseId, StudentSerialNumber: serialNumber }),
        function () { location.reload(true); });
}
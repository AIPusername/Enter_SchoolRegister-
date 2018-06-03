/* Definition of global variables */ 
var showHideAddCourse = $("#add-course-form-container");
var showHideRemoveCourse = $("#remove-course-form-container");

/* Definition of initial operation on the html pages */
$(document).ready(function () {
    showHideAddCourse.hide();
    showHideRemoveCourse.hide();
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
    var select = document.getElementById("removeName");
    var name = select.options[select.selectedIndex].text;
    var ects = select.options[select.selectedIndex].value;

    $.post("/Course/RemoveCourse", $.param({ Name: name, NumberOfECTS: ects }),
           function (getResult) { location.reload(true); });
}
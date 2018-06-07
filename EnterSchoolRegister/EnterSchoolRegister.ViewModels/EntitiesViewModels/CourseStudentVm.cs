using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.EntitiesViewModels
{
    public class CourseStudentVm
    {
        public CourseStudentVm(int cid, string cn, int ssn, string sl, string sf) {
            CourseId = cid;
            CourseName = cn;
            StudentSerialNumber = ssn;
            StudentLast = sl;
            StudentFirst = sf;
        }

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        [Display(Name = "Serial number")]
        public int StudentSerialNumber { get; set; }

        [Display(Name = "Last name")]
        public string StudentLast { get; set; }

        [Display(Name = "First name")]
        public string StudentFirst { get; set; }
    }
}

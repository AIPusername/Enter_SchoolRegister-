using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.EntitiesViewModels
{
    public class GradeVm
    {
        public GradeVm(string m, string d, string c, int cid, string cn, int ssn, string sl, string sf)
        {
            Mark = m;
            Date = d;
            Comment = c;
            CourseId = cid;
            CourseName = cn;
            StudentSerialNumber = ssn;
            StudentLast = sl;
            StudentFirst = sf;
        }

        [Display(Name = "Mark")]
        public string Mark { get; set; }

        [Display(Name = "Date")]
        public string Date { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

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

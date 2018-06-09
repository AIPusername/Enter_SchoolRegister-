using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.EntitiesViewModels
{
    public class GradeVm
    {
        [Display(Name = "Mark")]
        public float Mark { get; set; }

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

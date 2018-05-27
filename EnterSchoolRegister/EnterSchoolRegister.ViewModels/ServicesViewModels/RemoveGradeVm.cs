using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.ServicesViewModels
{
    public class RemoveGradeVm
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public int StudentSerialNumber { get; set; }

        [Required]
        public float Mark { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
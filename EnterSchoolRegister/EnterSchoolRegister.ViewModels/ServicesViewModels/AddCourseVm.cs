using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.ServicesViewModels
{
    public class AddCourseVm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int NumberOfECTS { get; set; }

        public int LecturesHours { get; set; }

        public int LaboratoriesHours { get; set; }

        public string Description { get; set; }
    }
}

using EnterSchoolRegister.BLL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.EntitiesViewModels
{
    public class CourseVm
    {
        [Display(Name = "CourseId")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "ECTS")]
        public int NumberOfECTS { get; set; }

        [Display(Name = "Hours of lecture")]
        public int LecturesHours { get; set; }

        [Display(Name = "Hours of laboratory")]
        public int LaboratoriesHours { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public int TeacherId { get; set; }

        public IEnumerable<Grade> Grades { get; set; }
    }
}

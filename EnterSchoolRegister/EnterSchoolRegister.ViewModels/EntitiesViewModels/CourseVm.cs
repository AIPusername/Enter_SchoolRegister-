using EnterSchoolRegister.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterSchoolRegister.ViewModels.EntitiesViewModels
{
    public class CourseVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfECTS { get; set; }

        public int LecturesHours { get; set; }

        public int LaboratoriesHours { get; set; }

        public string Description { get; set; }

        public int TeacherId { get; set; }

        public IEnumerable<Grade> Grades { get; set; }
    }
}

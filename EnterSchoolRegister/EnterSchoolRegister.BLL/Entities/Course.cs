using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnterSchoolRegister.BLL.Entities
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfECTS { get; set; }

        public int LecturesHours { get; set; }

        public int LaboratoriesHours { get; set; }

        public string Description { get; set; }

        [ForeignKey("User")]
        public int? TeacherId { get; }
    }
}

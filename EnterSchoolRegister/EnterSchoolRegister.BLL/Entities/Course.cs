using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnterSchoolRegister.BLL.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int NumberOfECTS { get; set; }

        public int LecturesHours { get; set; }

        public int LaboratoriesHours { get; set; }

        public string Description { get; set; }

        [Required]
        [ForeignKey("User")]
        public int TeacherId { get; set; }

        public User Teacher { get; set; }

        public ICollection<CourseStudent> CourseStudent { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}

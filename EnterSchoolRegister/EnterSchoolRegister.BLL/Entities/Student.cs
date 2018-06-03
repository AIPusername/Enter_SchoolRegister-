using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnterSchoolRegister.BLL.Entities
{
    public class Student
    {
        [Key]
        public int SerialNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [ForeignKey("Parent")]
        public int ParentId { get; set; }

        public User Parent { get; set; }

        public ICollection<CourseStudent> CourseStudent { get; set; }
        public ICollection<Grade> Grades { get; set; }

        public bool Active { get; set; }
    }
}

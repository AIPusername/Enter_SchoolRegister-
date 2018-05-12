using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnterSchoolRegister.BLL.Entities
{
    public class Grade
    {
        [Required]
        public float Mark { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Comment { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentSerialNumber { get; set; }

        public Student Student { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnterSchoolRegister.BLL.Entities
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public float Mark { get; set; }

        [Required]
        public string Date { get; set; }

        public string Comment { get; set; }

        [ForeignKey("Course")]
        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [ForeignKey("Student")]
        [Required]
        public int StudentSerialNumber { get; set; }
        public Student Student { get; set; }

        public bool Active { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnterSchoolRegister.BLL.Entities
{
    public class Grade
    {   
        public float Mark { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public Course Course { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Student Student { get; set; }

        [ForeignKey("Student")]
        public int StudentSerialNumber { get; set; }
    }
}

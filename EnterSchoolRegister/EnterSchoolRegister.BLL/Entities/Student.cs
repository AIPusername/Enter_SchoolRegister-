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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; }

        public User Parent { get; set; }
    }
}

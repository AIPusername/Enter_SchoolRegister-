using System;
using System.Collections.Generic;
using System.Text;

namespace EnterSchoolRegister.BLL.Entities
{
    class Student
    {
        public int SerialNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ParentId { get; set; } //how to create a foreign key?
    }
}

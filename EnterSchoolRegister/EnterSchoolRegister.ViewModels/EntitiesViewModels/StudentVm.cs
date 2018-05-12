using EnterSchoolRegister.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterSchoolRegister.ViewModels.EntitiesViewModels
{
    public class StudentVm
    {
        public int SerialNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ParentId { get; set; }

        public IEnumerable<Grade> Grades { get; set; }
    }
}

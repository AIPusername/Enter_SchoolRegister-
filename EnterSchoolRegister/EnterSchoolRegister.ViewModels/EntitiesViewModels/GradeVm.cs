using System;
using System.Collections.Generic;
using System.Text;

namespace EnterSchoolRegister.ViewModels.EntitiesViewModels
{
    public class GradeVm
    {
        public float Mark { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public int CourseId { get; set; }

        public int StudentSerialNumber { get; set; }
    }
}

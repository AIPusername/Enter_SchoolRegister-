using EnterSchoolRegister.BLL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.EntitiesViewModels
{
    public class StudentVm
    {
        [Display(Name = "Serial number")]
        public int SerialNumber { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Parent id number")]
        public int ParentId { get; set; }
    }
}

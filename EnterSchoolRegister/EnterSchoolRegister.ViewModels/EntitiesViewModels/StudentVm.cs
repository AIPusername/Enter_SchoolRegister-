using EnterSchoolRegister.BLL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.EntitiesViewModels
{
    public class StudentVm
    {
        public StudentVm() { }

        public StudentVm(int sn, string fn, string ln, int pid)
        {
            SerialNumber = sn;
            FirstName = fn;
            LastName = ln;
            ParentId = pid;
        }


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

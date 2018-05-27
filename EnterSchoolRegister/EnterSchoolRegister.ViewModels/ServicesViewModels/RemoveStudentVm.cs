using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.ServicesViewModels
{
    public class RemoveStudentVm
    {
        [Required]
        public int SerialNumber { get; set; }
    }
}

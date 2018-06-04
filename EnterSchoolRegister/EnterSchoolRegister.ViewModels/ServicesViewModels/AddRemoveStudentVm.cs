using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.ServicesViewModels
{
    public class AddRemoveStudentVm
    {
        [JsonProperty("FirstName")]
        [Required]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        [Required]
        public string LastName { get; set; }

        [Required]
        public int ParentId { get; set; }
    }
}

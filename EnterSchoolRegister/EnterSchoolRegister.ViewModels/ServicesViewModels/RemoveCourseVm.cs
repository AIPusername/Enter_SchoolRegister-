using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.ServicesViewModels
{
    public class RemoveCourseVm
    {
        [JsonProperty("Name")]
        [Required]
        public string Name { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [JsonProperty("NumberOfECTS")]
        [Required]
        public int NumberOfECTS { get; set; }
    }
}

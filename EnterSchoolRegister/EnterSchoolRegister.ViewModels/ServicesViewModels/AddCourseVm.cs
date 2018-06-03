using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.ServicesViewModels
{
    public class AddCourseVm
    {
        [JsonProperty("Name")]
        [Required]
        public string Name { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [JsonProperty("NumberOfECTS")]
        [Required]
        public int NumberOfECTS { get; set; }

        [JsonProperty("LecturesHours")]
        public int LecturesHours { get; set; }

        [JsonProperty("LaboratoriesHours")]
        public int LaboratoriesHours { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}

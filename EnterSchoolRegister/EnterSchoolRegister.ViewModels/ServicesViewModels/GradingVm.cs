using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.ServicesViewModels
{
    public class GradingVm
    {
        [JsonProperty("StudentSerialNumber")]
        [Required]
        public int StudentSerialNumber { get; set; }

        [JsonProperty("CourseId")]
        [Required]
        public int CourseId { get; set; }

        [JsonProperty("Mark")]
        [Required]
        public string Mark { get; set; }

        [JsonProperty("Date")]
        [Required]
        public string Date { get; set; }

        [JsonProperty("Comment")]
        public string Comment { get; set; }
    }
}

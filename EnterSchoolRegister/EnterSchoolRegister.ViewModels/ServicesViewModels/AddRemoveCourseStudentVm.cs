using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterSchoolRegister.ViewModels.ServicesViewModels
{
    public class AddRemoveCourseStudentVm
    {
        [JsonProperty("CourseId")]
        [Required]
        public int CourseId { get; set; }

        [JsonProperty("StudentSerialNumber")]
        [Required]
        public int StudentSerialNumber { get; set; }
    }
}

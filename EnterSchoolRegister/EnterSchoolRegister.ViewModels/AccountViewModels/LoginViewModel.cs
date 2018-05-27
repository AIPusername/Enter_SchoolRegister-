using System.ComponentModel.DataAnnotations;

namespace EnterSchoolRegister.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Teacher")]
        public bool Teacher { get; set; }

        [Display(Name = "Parent")]
        public bool Parent { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EnterSchoolRegister.ViewModels.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

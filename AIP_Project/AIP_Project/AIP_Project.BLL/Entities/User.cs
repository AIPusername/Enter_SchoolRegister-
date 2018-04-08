using Microsoft.AspNetCore.Identity;

namespace AIP_Project.BLL.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }
    }
}

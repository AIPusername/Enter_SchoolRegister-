using Microsoft.AspNetCore.Identity;

namespace EnterSchoolRegister.BLL.Entities
{
    public class User : IdentityUser<int>
    {
        public enum Roles { Teacher, Parent }
        private Roles role;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public User(Roles role) { this.role = role; }

        public string GetRole() { return role.ToString(); }
    }
}

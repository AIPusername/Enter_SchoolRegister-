using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace EnterSchoolRegister.BLL.Entities
{
    public class User : IdentityUser<int>
    {
        public enum Roles { Teacher, Parent }
        private string role;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public User(Roles role) { this.role = role.ToString(); }

        public string GetRole() { return role; }

        public ICollection<UserRole> UserRole { get; set; }
    }
}

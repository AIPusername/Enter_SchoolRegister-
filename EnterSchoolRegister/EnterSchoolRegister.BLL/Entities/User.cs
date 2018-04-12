using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace EnterSchoolRegister.BLL.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> UsersRoles { get; set; }
    }
}

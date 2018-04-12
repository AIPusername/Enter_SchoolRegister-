using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EnterSchoolRegister.BLL.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role() { }
        public Role(string name) : base(name) { }
    }
}

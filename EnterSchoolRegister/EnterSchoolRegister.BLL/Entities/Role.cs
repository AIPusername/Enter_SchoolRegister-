using Microsoft.AspNetCore.Identity;

namespace EnterSchoolRegister.BLL.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role()
        { }
        public Role(string name) : base(name) { }

        public ICollection<UserRole> UserRole { get; set; }
    }
}

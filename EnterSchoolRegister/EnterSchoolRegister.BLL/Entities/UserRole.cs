using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnterSchoolRegister.BLL.Entities
{
   public class UserRole
    {
        public Role Role { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}

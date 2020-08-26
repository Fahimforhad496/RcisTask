using System;
using System.Collections.Generic;
using System.Text;
using DLL.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DLL.Models
{
    public class AppRole : IdentityRole<int>
    {
        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}

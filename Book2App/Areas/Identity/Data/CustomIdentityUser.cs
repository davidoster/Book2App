using System;
using Microsoft.AspNetCore.Identity;

namespace Book2App.Areas.Identity.Data
{
    public class CustomIdentityUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
    }
}

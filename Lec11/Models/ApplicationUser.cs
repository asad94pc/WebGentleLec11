using Microsoft.AspNetCore.Identity;
using System;

namespace Lec11.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string  FirstName { get; set; }
        public string  LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

    }
}

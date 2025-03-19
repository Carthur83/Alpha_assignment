﻿using Microsoft.AspNetCore.Identity;

namespace Data.Models
{
    public class AppUser : IdentityUser
    {
        [ProtectedPersonalData]
        public string FirstName { get; set; } = null!;

        [ProtectedPersonalData]
        public string LastName { get; set; } = null!;
    }
}

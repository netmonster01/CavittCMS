using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //[NotMapped]
        //public List<string> Roles { get; set; }
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public string AvatarImage { get; set; }
        public string AvatarImageType { get; set; }

        //public static explicit operator User(ApplicationUser incomingUser)
        //{
        //    return new User
        //    {
        //        Id = incomingUser.Id,
        //        AvatarImageBase64 = incomingUser.AvatarImage,
        //        Email = incomingUser.Email,
        //        FirstName = incomingUser.FirstName,
        //        LastName = incomingUser.LastName,
        //        IsAdmin = incomingUser.Roles?.Any() ?? incomingUser.Roles.Contains("Admin"),
        //        AvatarImageType = incomingUser.AvatarImageType,
        //        UserName = incomingUser.UserName
        //    }; // do your mapping
        //}
    }
}

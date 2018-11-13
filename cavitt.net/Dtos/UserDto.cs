using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public bool IsAdmin { get; set; }
        [NotMapped]
        public string AvatarImageBase64 { get; set; }
        public string AvatarImage { get; set; }
        public string AvatarImageType { get; set; }
    }
}

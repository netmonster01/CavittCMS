using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cavitt.net.Converters
{
    public class UserDtoToApplicationUserConverter : IConverter<UserDto, ApplicationUser>
    {

        private readonly ILoggerRepository _loggerRepository;

        public UserDtoToApplicationUserConverter(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }

        public ApplicationUser Convert(UserDto sourceUser)
        {
            if (sourceUser == null)
            { return null; }

            try
            {
                List<ApplicationUserRole> roles = new List<ApplicationUserRole>();
                if (sourceUser.Roles.Any())
                {
                    foreach (var item in sourceUser.Roles)
                    {
                        roles.Add(new ApplicationUserRole { Role = new ApplicationRole() { Name = item } });
                    }

                }
                
                ApplicationUser user = new ApplicationUser
                {
                  AvatarImage = sourceUser.AvatarImage,
                  AvatarImageType = sourceUser.AvatarImageType,
                  Email = sourceUser.Email,
                  FirstName = sourceUser.FirstName,
                  Id = sourceUser.Id,
                  LastName = sourceUser.LastName,
                  UserRoles = roles,
                  UserName = sourceUser.UserName
                };

                return user;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
            }

            return null;
        }
    }
}

using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using System;
using System.Linq;

namespace cavitt.net.Converters
{
    public class ApplicationUserToUserDtoConverter : IConverter<ApplicationUser, UserDto>
    {

        private readonly ILoggerRepository _loggerRepository;

        public ApplicationUserToUserDtoConverter(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }

        public UserDto Convert(ApplicationUser sourceUser)
        {
            if (sourceUser == null)
            { return null; }

            try
            {
                UserDto user = new UserDto
                {
                  AvatarImage = sourceUser.AvatarImage,
                  AvatarImageType = sourceUser.AvatarImageType,
                  Email = sourceUser.Email,
                  FirstName = sourceUser.FirstName,
                  Id = sourceUser.Id,
                  LastName = sourceUser.LastName,
                  Roles = sourceUser.UserRoles.Select(r =>r.Role.Name).ToList(),
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

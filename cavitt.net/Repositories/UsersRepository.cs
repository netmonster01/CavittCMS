using cavitt.net.Data;
using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static cavitt.net.CustomEnums;

namespace cavitt.net.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILoggerRepository _loggerRepository;
        private readonly IConverter<ApplicationUser, UserDto> _converter;

        public UsersRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _loggerRepository = _serviceProvider.GetRequiredService<ILoggerRepository>();
            _converter = _serviceProvider.GetRequiredService<IConverter<ApplicationUser, UserDto>>();
        }

        public List<UserDto> GetAllUsersAsync()
        {
            List<UserDto> users = new List<UserDto>();
            _loggerRepository.Write(LogType.Info, string.Format("GetAllUsersAsync called:"));
            try
            {
                var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = _serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                users = userManager.Users.Select(user => new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = GetRolesByEmail(user.Email),
                    IsAdmin = IsAdmin(user.Email),
                    AvatarImage = user.AvatarImage,
                    AvatarImageType = user.AvatarImageType

                }).ToList();

                //users = userManager.Users.Include(u => u.UserRoles).Select(user => new User {
                //    Id = user.Id,
                //    FirstName = user.FirstName,
                //    LastName = user.LastName,
                //    UserName = user.UserName,
                //    Email = user.Email,
                //}).ToList();

                //// loop
             
                //try
                //{
                //    foreach (var user in users)
                //    {

                //        ApplicationUser u = new ApplicationUser { UserName = user.UserName };

                //        var usr = userManager.FindByEmailAsync(user.Email);
                //        //var r = userManager.Users.Select(u=> u.r)
                //        //usr.Rol
                //        //user.Roles = await roleManager.get
                //    }
                //}
                //catch (Exception)
                //{
                //    // empty may not even need this.
                //}
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
            }
            return users;
        }

        private bool IsAdmin(string email)
        {
            var roles = GetRolesByEmail(email);
            if(roles.Any() && roles.Contains("Admin"))
            {
                return true;
            }
            return false;
            //return GetRolesByEmail(email) != null ?  
        }

        public UserDto UpdateProfile(UserDto user)
        {
            UserDto returnUser = new UserDto();
            if (user == null || user.UserName == null)
            {
                throw new Exception("user is required");
            }

            try
            {

                ApplicationUser profile = _dbContext.Users.Where(c => c.UserName == user.UserName).FirstOrDefault();
                if (profile != null)
                {
                    // convert to a 
                    ApplicationUser userToUpdate = new ApplicationUser
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = profile.UserName,
                        AvatarImage = user.AvatarImage,
                        AvatarImageType = user.AvatarImageType
                    };

                    UpdateProfile(userToUpdate);
                    returnUser = _converter.Convert(userToUpdate);
                }
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
            return returnUser;

        }
        public ApplicationUser UpdateProfile(ApplicationUser user)
        {
            if (user == null || user.UserName == null)
            {
                throw new Exception("user is required");

            }

            ApplicationUser profile = new ApplicationUser();
            try
            {
                //user.AvatarImage = Convert.FromBase64String(user.AvatarImageBas64);
                profile = _dbContext.Users.Where(c => c.UserName == user.UserName).FirstOrDefault();
                if (profile != null)
                {
                    profile.FirstName = user.FirstName;
                    profile.LastName = user.LastName;
                    profile.Email = user.Email;
                    profile.UserName = user.UserName;
                    profile.AvatarImage = user.AvatarImage;
                    profile.AvatarImageType = user.AvatarImageType;

                    _dbContext.Entry(profile).State = Microsoft.EntityFrameworkCore.EntityState.Modified; ;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
            }

            return profile;
        }

        private List<string> GetRolesByEmail(string email)
        {
            List<string> roles = new List<string>();
            try
            {
                roles = (from ur in _dbContext.UserRoles
                         join r in _dbContext.Roles on ur.RoleId equals r.Id
                         join u in _dbContext.Users on ur.UserId equals u.Id
                         where u.Email == email
                         select (r.Name)).ToList();
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
            }
            return roles;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cavitt.net.Models;
using Microsoft.Extensions.Configuration;
using cavitt.net.Interfaces;
using cavitt.net.Dtos;
using static cavitt.net.CustomEnums;

namespace cavitt.net.Repositories
{
    public class RoleRepository : IRolesRepository
    {
  
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoggerRepository _loggerRepository;
        private RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleRepository(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _loggerRepository = _serviceProvider.GetRequiredService<ILoggerRepository>();
            _roleManager = _serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            _userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        public async Task<bool> AssignRole(string userName, string roleName)
        {
            bool bDidAdd = false;

            var user = await _userManager.FindByEmailAsync(userName);
           
            var  status = await _userManager.AddToRoleAsync(user, roleName);
            if (status.Succeeded)
            {
                bDidAdd = true;
            }

            return bDidAdd;
        }

        public async Task<bool> CreateInitialRoles()
        {
            bool didCreateRoles = false;
            // adding custom roles

            string[] roleNames = Enum.GetNames(typeof(RoleType));

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                // creating the roles and seeding them to the database
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await _roleManager.CreateAsync(new ApplicationRole { Name = roleName});
                }
                didCreateRoles = true;
            }

            return didCreateRoles;
        }

        public List<RoleDto> GetAllRoles()
        {
            List<RoleDto> roles = new List<RoleDto>();

            try
            {

                roles = _roleManager.Roles.Select(role => new RoleDto
                {
                    Id = role.Id,
                    RoleName = role.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
            }

            return roles;
        }

        public async Task<List<string>> GetUserRolesAsync(string userName)
        {
            List<string> userRoles = new List<string>();
            ApplicationUser user = new ApplicationUser {
                UserName = userName
            };

            try
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Any())
                {
                    userRoles = roles.Select(x => x).ToList();
                }
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }

            return userRoles;
        }
    }
}

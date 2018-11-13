using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static cavitt.net.CustomEnums;

namespace cavitt.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConverter<ApplicationUser, UserDto> _converter;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppSettings _appSettings;
        private readonly IRolesRepository _rolesRepository;
        private readonly ILoggerRepository _loggerRepository;
        private readonly IUsersRepository _usersRepository;

        public AccountController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _converter = _serviceProvider.GetRequiredService<IConverter<ApplicationUser, UserDto>>();
            _roleManager = _serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            _appSettings = _serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
            _rolesRepository = _serviceProvider.GetRequiredService<IRolesRepository>();
            _loggerRepository = _serviceProvider.GetRequiredService<ILoggerRepository>();
            _usersRepository = _serviceProvider.GetRequiredService<IUsersRepository>();
            _signInManager = _serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<UserDto> Login([FromBody] LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                _loggerRepository.Write(LogType.Pass, string.Format("User: {0} Logged In.", appUser.UserName));
                return await GenerateJwtToken(model.Email, appUser);
            }

            _loggerRepository.Write(LogType.Fail, string.Format("User: {0} Failed to Log In.", model.Email));
            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpGet]
        [Route("IsAuthenticated")]
        [AllowAnonymous]
        public IActionResult IsAuthenticated()
        {
            if (User.Identity.IsAuthenticated)
            {
                _loggerRepository.Write(LogType.Pass, string.Format("AuthCheck"));
            }
            return Ok(User.Identity.IsAuthenticated);
        }


        [HttpPut]
        [Route("UpdateProfile")]
        [Produces("application/json")]
        [Consumes("application/json", "application/json-patch+json", "multipart/form-data")]
        [AllowAnonymous]
        public IActionResult UpdateProfile([FromBody] UserDto user)
        {

            UserDto updatedUser = _usersRepository.UpdateProfile(user);

            return Ok(updatedUser);
        }

        [HttpGet]
        [Route("Logout")]
        [AllowAnonymous]
        public async void Logout()
        {
            _loggerRepository.Write(LogType.Info, string.Format("User: {0} LoggedOut!", User.Identity.Name));
            await _signInManager.SignOutAsync();
        }


        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                _loggerRepository.Write(LogType.Info, string.Format("New User: {0} Registered.", model.Email));
                return await GenerateJwtToken(model.Email, user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpGet]
        [Route("Users")]
        //[Authorize]
        [AllowAnonymous]
        public IEnumerable<UserDto> GetAll()
        {
            var userRepo = _serviceProvider.GetRequiredService<IUsersRepository>();
            var users = userRepo.GetAllUsersAsync();
            // return users without passwords
            return users.Select(x =>
            {
                x.Password = null;
                return x;
            });
        }

        [HttpGet]
        [Route("User")]
        //[Authorize]
        [AllowAnonymous]
        public async Task<UserDto> GetUserInfoAsync()
        {

            UserDto user = new UserDto();
            if (User.Identity.IsAuthenticated)
            {
                string uName = User.Identity.IsAuthenticated ? User.Identity.Name : null;

                if (!string.IsNullOrEmpty(uName))
                {
                    var roles = await _rolesRepository.GetUserRolesAsync(uName);

                    user.UserName = uName;
                    user.Email = uName;
                    if (roles.Any())
                    {
                        user.Roles.AddRange(roles);
                    }
                }

            }
            return user;
        }

        /// <summary>
        /// Generate jsonToken
        /// </summary>
        /// <param name="email"></param>
        /// <param name="appUser"></param>
        /// <returns></returns>
        private async Task<UserDto> GenerateJwtToken(string email, ApplicationUser appUser)
        {
            UserDto user = new UserDto();

            user.UserName = appUser.UserName;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, email)
            };

            // get the users roles.
            var roles = await _userManager.GetRolesAsync(appUser);
            var u = await _userManager.FindByEmailAsync(appUser.UserName);
            if (u != null)
            {
                user.FirstName = u.FirstName;
                user.LastName = u.LastName;
            }
            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                //            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                //    new Claim(ClaimTypes.GivenName, user.FirstName)
                //});
            }

            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                //            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                //     new Claim(ClaimTypes.Surname, user.LastName),
                //});
            }
            if (roles.Any())
            {
                claims.AddRange(roles.Select(role => new Claim("Role", role)));
                user.Roles = roles.ToList();
                user.IsAdmin = roles.Contains("Admin");
            }

            // check for image.


            //security key
            var securityKey = Encoding.UTF8.GetBytes(_appSettings.Secret);//  Encoding.ASCII.GetBytes(_appSettings.Secret);
            //var signingKey = new SymmetricSecurityKey(securityKey);

            // SymmetricSecurityKey
            var symetricSecurityKey = new SymmetricSecurityKey(securityKey);

            // Signing Credentials
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            // Create Token
            var token = new JwtSecurityToken(
                issuer: _appSettings.Issuer, //"netmon.in",
                audience: _appSettings.Audience, // "readers",
                expires: DateTime.Now.AddHours(7),
                signingCredentials: signingCredentials,
                claims: claims
                );

            var sToken = new JwtSecurityTokenHandler().WriteToken(token);
            user.Token = sToken;
            user.Email = appUser.UserName;
            user.Password = null;
            user.AvatarImage = appUser.AvatarImage;
            user.AvatarImageType = appUser.AvatarImageType;
            user.Id = appUser.Id;
            _loggerRepository.Write(LogType.Pass, string.Format("Generated Token {0} for User: {1} Registered.", user.Token, user.Email));
            return user;

        }
    }

}

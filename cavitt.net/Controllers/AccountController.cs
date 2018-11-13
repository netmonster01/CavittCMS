using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace cavitt.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConverter<ApplicationUser, UserDto> _converter;

        public AccountController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _converter = _serviceProvider.GetRequiredService<IConverter<ApplicationUser, UserDto>>();
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
                //await _signInManager.SignInAsync(user, false);
                //return await GenerateJwtToken(model.Email, user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        // GET: api/Account
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Account/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("Users")]

        public IEnumerable<UserDto> GetUsers()
        {
            var users = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).Select(user => _converter.Convert(user)).ToList();

            return users;
        }
        // POST: api/Account
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

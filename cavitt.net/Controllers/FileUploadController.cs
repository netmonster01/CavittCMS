using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using cavitt.net.Interfaces;
using cavitt.net.Data;
using cavitt.net.Models;
using static cavitt.net.CustomEnums;
using cavitt.net.Dtos;
using Microsoft.AspNetCore.Http;

namespace cavitt.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json", "application/json-patch+json", "multipart/form-data")]
    public class FileUploadController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoggerRepository _loggerRepository;
        private readonly IUsersRepository _userRepository;
        private readonly ApplicationDbContext _db;
        private readonly IConverter<ApplicationUser, UserDto> _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public FileUploadController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _loggerRepository = _serviceProvider.GetRequiredService<ILoggerRepository>();
            _db = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _mapper = _serviceProvider.GetRequiredService<IConverter<ApplicationUser, UserDto>>();
            _userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _userRepository = _serviceProvider.GetRequiredService<IUsersRepository>();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfileImage([FromBody] string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return StatusCode(500, "userName required.");
            }
            bool didSave = false;
            var file = Request.Form.Files[0];
            var user = _userManager.Users.Where(c=>c.UserName == userName).FirstOrDefault();

            if(user == null)
            {
                return StatusCode(500, "user not found");
            }

            // Convert.FromBase64String(user.AvatarImageBas64);

            //BlogImage image = new BlogImage
            //{
            //    Title = file.FileName.Replace(" ", "-")
            //};
          
            // var blogsRepo = _serviceProvider.GetRequiredService<IBlog>();
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    user.AvatarImage = memoryStream.ToString();
                    _userRepository.UpdateProfile(user);
                    _loggerRepository.Write(LogType.Info, "Added user Image");
                    didSave = true;
                }
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
            }
            //blogsRepo.

            return Ok(didSave);
        }


        [HttpPost]
        [Route("upload")]
        public void PostFile(IFormFile file)
        {
            //TODO: Save file

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    var e =Convert.ToString(fileBytes);
                    string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }
        }

        [HttpPost]
        [Route("Project/Thumbnail")]
        public void PostProjectThumbnailFile([FromForm]int projectId, [FromForm] IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    var e = Convert.ToString(fileBytes);
                    string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }
        }
    }
}
using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Converters
{
    public class ProjectConverter : IConverter<Project, ProjectDto>
    {
        private readonly ILoggerRepository _loggerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConverter<Comment, CommentDto> _converter;
        public ProjectConverter(ILoggerRepository loggerRepository, UserManager<ApplicationUser> userManager, IConverter<Comment, CommentDto> converter)
        {
            _loggerRepository = loggerRepository;
            _userManager = userManager;
            _converter = converter;
        }

        public ProjectDto Convert(Project source_object)
        {
            throw new NotImplementedException();
        }

        public Project Convert(ProjectDto source_object)
        {
            throw new NotImplementedException();
        }
    }
}

using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Converters
{
    public class ProjectCategoryConverter : IConverter<ProjectCategory, ProjectCategoryDto>
    {
        private readonly ILoggerRepository _loggerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConverter<ProjectCategory, ProjectCategoryDto> _converter;

        public ProjectCategoryConverter(ILoggerRepository loggerRepository, UserManager<ApplicationUser> userManager, IConverter<ProjectCategory, ProjectCategoryDto> converter)
        {
            _loggerRepository = loggerRepository;
            _userManager = userManager;
            _converter = converter;
        }

        public ProjectCategoryDto Convert(ProjectCategory source_object)
        {
            throw new NotImplementedException();
        }

        public ProjectCategory Convert(ProjectCategoryDto source_object)
        {
            throw new NotImplementedException();
        }
    }
}

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

        public ProjectCategoryConverter(ILoggerRepository loggerRepository, UserManager<ApplicationUser> userManager)
        {
            _loggerRepository = loggerRepository;
            _userManager = userManager;
        }

        public ProjectCategoryDto Convert(ProjectCategory source_object)
        {
            if (source_object == null)
            { return null; }

            try
            {

                ProjectCategoryDto project = new ProjectCategoryDto
                {
                    CategoryDescription = source_object.CategoryDescription,
                    CategoryId = source_object.CategoryId,
                    CategoryName = source_object.CategoryName,
                    Thumbnail = string.Format("data:image/jpg;base64,{0}", source_object.Thumbnail),
                };

                return project;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }

        public ProjectCategory Convert(ProjectCategoryDto source_object)
        {
            if (source_object == null)
            { return null; }

            try
            {
                ProjectCategory project = new ProjectCategory
                {
                    CategoryDescription = source_object.CategoryDescription,
                    CategoryId = source_object.CategoryId,
                    CategoryName = source_object.CategoryName,
                    Thumbnail = source_object.Thumbnail,
                };

                return project;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }
    }
}

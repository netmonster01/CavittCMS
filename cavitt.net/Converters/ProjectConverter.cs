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
            if (source_object == null)
            { return null; }

            try
            {

                ProjectDto project = new ProjectDto
                {
                    Active = source_object.Active,
                    CategoryId = source_object.CategoryId,
                    Content = source_object.Content,
                    GitHubUrl = source_object.GitHubUrl,
                    Images = source_object.Images.Select( i => string.Format("data:image/jpg;base64,{0}", i.Base64Image)).ToList(),
                    Keywords = source_object.Keywords,
                    ProjectId = source_object.ProjectId,
                    Title = source_object.Title,
                    ThumbnailImage = string.Format("data:image/jpg;base64,{0}", source_object.ThumbnailImage)
                };

                return project;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }

        public Project Convert(ProjectDto source_object)
        {
            if (source_object == null)
            { return null; }

            try
            {

                Project project = new Project
                {
                    Active = source_object.Active,
                    CategoryId = source_object.CategoryId,
                    Content = source_object.Content,
                    GitHubUrl = source_object.GitHubUrl,
                    Images = source_object.Images.Select(i => new ProjectImage { Base64Image = i, ProjectId = source_object.ProjectId }).ToList(),
                    Keywords = source_object.Keywords,
                    ProjectId = source_object.ProjectId,
                    Title = source_object.Title,
                    ThumbnailImage = source_object.ThumbnailImage

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

using cavitt.net.Models;
using System;
using cavitt.net.Dtos;
using cavitt.net.Interfaces;
namespace cavitt.net.Converters
{
    public class ProjectImageConverter : IConverter<ProjectImage, ProjectImageDto>
    {
        private readonly ILoggerRepository _loggerRepository;

        public ProjectImageConverter(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }

        public ProjectImageDto Convert(ProjectImage source_object)
        {
            if (source_object == null)
            { return null; }

            try
            {

                ProjectImageDto projectImage = new ProjectImageDto
                {
                    Base64Image = source_object.Base64Image,
                    ImageId = source_object.ImageId,
                    ProjectId = source_object.ProjectId
                };

                return projectImage;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }

        public ProjectImage Convert(ProjectImageDto source_object)
        {
            if (source_object == null)
            { return null; }

            try
            {

                ProjectImage projectImage = new ProjectImage
                {
                    Base64Image = source_object.Base64Image,
                    ImageId = source_object.ImageId,
                    ProjectId = source_object.ProjectId
                };

                return projectImage;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }
    }
}

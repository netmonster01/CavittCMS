using cavitt.net.Data;
using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Repositories
{
    public class ProjectRepository : IProjectRepository
    {

        private readonly ILoggerRepository _loggerRepository;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConverter<Project, ProjectDto> _converter;
        private readonly IConverter<ProjectCategory, ProjectCategoryDto> _catConverter;

        public ProjectRepository(ApplicationDbContext applicationDbContext, ILoggerRepository loggerRepository, IConverter<Project, ProjectDto> converter, IConverter<ProjectCategory, ProjectCategoryDto> catConverter)
        {
            _applicationDbContext = applicationDbContext;
            _loggerRepository = loggerRepository;
            _converter = converter;
            _catConverter = catConverter;
        }

        public async Task<bool> AddProjectAsync(ProjectDto project)
        {
            try
            {
                _applicationDbContext.Projects.Add(_converter.Convert(project));
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return false;
            }
        }

        public Task<bool> CreateProjectCategories(ProjectCategoryDto projectCategory)
        {
            throw new NotImplementedException();
        }

        public ProjectDto GetProject(int projectID)
        {
            try
            {
                return _converter.Convert(_applicationDbContext.Projects.Where(p => p.ProjectId == projectID).Include(i => i.Images).FirstOrDefault());
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }

        public ProjectDto GetProject(string projectName)
        {
            try
            {
                return _converter.Convert(_applicationDbContext.Projects.Where(p => p.Title == projectName).Include(i => i.Images).FirstOrDefault());
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }

        public List<ProjectCategoryDto> GetProjectCategories()
        {
            try
            {
                return _applicationDbContext.ProjectCategories.Select(pc => _catConverter.Convert(pc)).ToList();
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return null;
            }
        }

        public List<ProjectDto> GetProjects()
        {
            try
            {
                return _applicationDbContext.Projects.Include(i => i.Images).Select(p => _converter.Convert(p)).ToList();
            }
            catch (Exception ex)
            {

                _loggerRepository.Write(ex);
                return null;
            }
        }

        public List<ProjectDto> GetProjectsByCategoryId(int categoryId)
        {
            try
            {
                return _applicationDbContext.Projects.Where(p => p.CategoryId == categoryId).Include(i => i.Images).Select(p => _converter.Convert(p)).ToList();
            }
            catch (Exception ex)
            {

                _loggerRepository.Write(ex);
                return null;
            }
        }

        public bool ProjectExists(int projectId)
        {
            return _applicationDbContext.Projects.Any(p => p.ProjectId == projectId);
        }

        public Task<bool> UdateProjectAsync(ProjectDto project)
        {
            throw new NotImplementedException();
        }
    }
}

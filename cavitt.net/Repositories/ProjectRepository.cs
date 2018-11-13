using cavitt.net.Data;
using cavitt.net.Interfaces;
using cavitt.net.Models;
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

        public ProjectRepository(ApplicationDbContext applicationDbContext, ILoggerRepository loggerRepository)
        {
            _applicationDbContext = applicationDbContext;
            _loggerRepository = loggerRepository;
        }

        public async Task<bool> AddProjectAsync(Project project)
        {

            try
            {
                _applicationDbContext.Projects.Add(project);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _loggerRepository.Write(ex);
                return false;
            }

        }

        public Project GetProject(int projectID)
        {
            return _applicationDbContext.Projects.Where(p => p.ProjectId == projectID).FirstOrDefault();
        }

        public Project GetProject(string projectName)
        {
           return _applicationDbContext.Projects.Where(p => p.Title == projectName).FirstOrDefault();
        }

        public List<Project> GetProjects()
        {

            try
            {
                List<Project> projects = new List<Project>();
                projects = _applicationDbContext.Projects.ToList();
                return projects;
            }
            catch (Exception ex)
            {

                _loggerRepository.Write(ex);
                return null;
            }

        }

        public Task<bool> UdateProjectAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public bool ProjectExists(int projectId)
        {
            return _applicationDbContext.Projects.Any(p => p.ProjectId == projectId);
        }
    }
}

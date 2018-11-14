using cavitt.net.Dtos;
using cavitt.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Interfaces
{
    public interface IProjectRepository
    {
        List<ProjectDto> GetProjects();
        ProjectDto GetProject(int projectID);
        ProjectDto GetProject(string projectName);
        Task<bool> AddProjectAsync(ProjectDto project);

        Task<bool> UdateProjectAsync(ProjectDto project);

        bool ProjectExists(int projectId);

        List<ProjectCategoryDto> GetProjectCategories();
        Task<bool> CreateProjectCategories(ProjectCategoryDto projectCategory);
    }
}

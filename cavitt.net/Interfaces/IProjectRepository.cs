using cavitt.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Interfaces
{
    public interface IProjectRepository
    {
        List<Project> GetProjects();
        Project GetProject(int projectID);
        Project GetProject(string projectName);
        Task<bool> AddProjectAsync(Project project);

        Task<bool> UdateProjectAsync(Project project);

        bool ProjectExists(int projectId);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cavitt.net.Dtos;
using cavitt.net.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cavitt.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // GET: api/Project
        [HttpGet]
        public IEnumerable<ProjectDto> Get()
        {
            return _projectRepository.GetProjects();
        }




        // GET: api/Project/5
        [HttpGet("{id}", Name = "Get")]
        public ProjectDto Get(int id)
        {
            return _projectRepository.GetProject(id);
        }

        // GET: api/Project/5
        [HttpGet]
        [Route("Category/{categoryId}")]
        public IEnumerable<ProjectDto> GetProjectByCategoryId(int categoryId)
        {
            return _projectRepository.GetProjectsByCategoryId(categoryId);
        }

        // POST: api/Project
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProjectDto project)
        {

          return Ok(await  _projectRepository.AddProjectAsync(project));
        }
        // POST: api/Project
        [HttpPost]
        [Route("Category")]
        public async Task<IActionResult> PostCategoryAsync([FromBody] ProjectCategoryDto projectCategory)
        {
            return Ok(await _projectRepository.AddProjectCategoriesAsync(projectCategory));
        }

        // PUT: api/Project/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException("NotImplemented");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException("NotImplemented");
        }


        [HttpGet]
        [Route("Categories")]
        public IEnumerable<ProjectCategoryDto> GetCategories()
        {
            return _projectRepository.GetProjectCategories();
        }
    }
}

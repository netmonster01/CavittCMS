using cavitt.net.Dtos;
using cavitt.net.Interface;
using Microsoft.AspNetCore.Mvc;

namespace cavitt.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository) {
                _dashboardRepository = dashboardRepository;
        }

        // GET: api/Dashboard
        [HttpGet]
        public DashboardDto Get()
        {
            return _dashboardRepository.GetDashboard();
        }

        //// GET: api/Dashboard/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Dashboard
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Dashboard/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

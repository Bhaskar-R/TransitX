using Microsoft.AspNetCore.Mvc;
using TransitX.Common;
using TransitX.Common.Repository;
using Route = TransitX.Common.Models.Route;

namespace TransitX.API.Controllers
{
    /// <summary>
    /// Controller for managing Route objects.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : BaseController<Route>
    {
        private readonly IRepository<Route> _routeRepository;

        public RouteController(IRepository<Route> routeRepository) : base(routeRepository)
        {
            _routeRepository = routeRepository;
        }

        /// <summary>
        /// Calculates the total distance of all routes.
        /// </summary>
        /// <returns>The total distance of all routes.</returns>
        [HttpGet("totaldistance")]
        public async Task<IActionResult> GetTotalDistance()
        {
            try
            {
                var routes = await _routeRepository.GetAll();
                var totalDistance = CommonUtilities.CalculateTotalDistance(routes);
                return Ok(totalDistance);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest("Error retrieving total distance: " + ex.Message);
            }
        }
    }
}

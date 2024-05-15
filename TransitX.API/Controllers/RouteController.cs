using Microsoft.AspNetCore.Mvc;
using TransitX.Common;
using TransitX.Common.Service;
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
        private readonly IService<Route> _routeService;

        public RouteController(IService<Route> routeService) : base(routeService)
        {
            _routeService = routeService;
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
                var routes = await _routeService.GetAll();
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

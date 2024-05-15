using Microsoft.AspNetCore.Mvc;
using TransitX.Common;
using TransitX.Common.Models;
using TransitX.Common.Service;

namespace TransitX.API.Controllers
{
    /// <summary>
    /// Controller for managing Vehicle objects.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : BaseController<Vehicle>
    {
        private readonly IService<Vehicle> _vehicleService;

        public VehicleController(IService<Vehicle> vehicleService) : base(vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Serializes a Vehicle object to JSON.
        /// </summary>
        /// <remarks>This endpoint serializes the provided Vehicle object to JSON format.</remarks>
        /// <param name="vehicle">The Vehicle object to serialize.</param>
        /// <returns>The JSON representation of the serialized Vehicle object.</returns>
        [HttpPost("serialize")]
        public ActionResult<string> SerializeVehicle([FromBody] Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return BadRequest("Vehicle object is null.");
            }

            string json = CommonUtilities.SerializeToJson(vehicle);
            return Ok(json);
        }

        /// <summary>
        /// Deserializes a JSON string to a Vehicle object.
        /// </summary>
        /// <remarks>This endpoint deserializes the provided JSON string to a Vehicle object.</remarks>
        /// <param name="json">The JSON string representing the Vehicle object.</param>
        /// <returns>The deserialized Vehicle object.</returns>
        [HttpPost("deserialize")]
        public ActionResult<Vehicle> DeserializeVehicle([FromBody] string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return BadRequest("JSON string is null or empty.");
            }

            Vehicle vehicle = CommonUtilities.DeserializeFromJson<Vehicle>(json);
            return Ok(vehicle);
        }
    }
}

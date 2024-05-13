using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TransitX.Common.Models
{
    /// <summary>
    /// Represents a route between two locations.
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Gets or sets the unique identifier of the route.
        /// </summary>
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the starting location of the route.
        /// </summary>
        [BsonElement("startLocation")]
        public Coordinate StartLocation { get; set; }

        /// <summary>
        /// Gets or sets the ending location of the route.
        /// </summary>
        [BsonElement("endLocation")]
        public Coordinate EndLocation { get; set; }

        /// <summary>
        /// Gets the distance of the route in kilometers.
        /// </summary>
        [BsonElement("distance"), BsonRepresentation(BsonType.Double)]
        public double Distance
        {
            get
            {
                if (StartLocation == null || EndLocation == null)
                {
                    throw new InvalidOperationException("Start and end locations must be set to calculate distance.");
                }

                return StartLocation.GetDistanceInKilometers(EndLocation);
            }
        }

        // Constant speed in km/h
        private const double AverageSpeed = 60;

        /// <summary>
        /// Gets the estimated travel time of the route in hours.
        /// </summary>
        [BsonElement("estimatedTravelTime"), BsonRepresentation(BsonType.Double)]
        public double EstimatedTravelTime => Distance / AverageSpeed;
    }
}

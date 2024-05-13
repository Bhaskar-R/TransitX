namespace TransitX.Common.Models
{
    /// <summary>
    /// Represents a geographical coordinate with latitude and longitude.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Gets the latitude component of the coordinate.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Gets the longitude component of the coordinate.
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// Gets the name associated with the coordinates (optional).
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class with the specified latitude and longitude.
        /// </summary>
        /// <param name="latitude">The latitude component of the coordinate.</param>
        /// <param name="longitude">The longitude component of the coordinate.</param>
        /// <param name="name">The name associated with the coordinate (optional).</param>
        public Coordinate(double latitude, double longitude, string? name = null)
        {
            Latitude = latitude;
            Longitude = longitude;
            Name = name;
        }
    }
}

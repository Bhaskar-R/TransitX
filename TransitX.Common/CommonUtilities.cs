using Newtonsoft.Json;
using TransitX.Common.Models;

namespace TransitX.Common
{
    /// <summary>
    /// Provides common utility methods for the TransitX application.
    /// </summary>
    public static class CommonUtilities
    {
        /// <summary>
        /// Serializes an object to a JSON string.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A JSON string representing the object.</returns>
        public static string SerializeToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Deserializes a JSON string to an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>An object of type T deserialized from the JSON string.</returns>
        /// <exception cref="JsonException">Thrown when the JSON string is malformed or incompatible with the target type.</exception>
        public static T DeserializeFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Calculates the total distance traveled given a list of routes.
        /// </summary>
        /// <param name="routes">The list of routes.</param>
        /// <returns>The total distance traveled.</returns>
        public static double CalculateTotalDistance(IEnumerable<Route> routes)
        {
            double totalDistance = 0;
            foreach (var route in routes)
            {
                totalDistance += route.Distance;
            }
            return totalDistance;
        }

        /// <summary>
        /// Calculates the estimated arrival time based on the departure time and estimated travel time.
        /// </summary>
        /// <param name="departureTime">The departure time.</param>
        /// <param name="estimatedTravelTime">The estimated travel time.</param>
        /// <returns>The estimated arrival time.</returns>
        public static TimeSpan CalculateEstimatedArrivalTimeOfDay(DateTime departureTime, TimeSpan estimatedTravelTime)
        {
            return departureTime.TimeOfDay + estimatedTravelTime;
        }

        /// <summary>
        /// Validates the coordinates of a given location.
        /// </summary>
        /// <param name="coordinate">The coordinate to validate.</param>
        /// <returns>True if the coordinates are valid, otherwise false.</returns>
        public static bool ValidateCoordinates(this Coordinate coordinate)
        {
            return coordinate.Latitude >= -90.0 && coordinate.Latitude <= 90.0 &&
                   coordinate.Longitude >= -180.0 && coordinate.Longitude <= 180.0;
        }

        private const double EarthRadiusInKilometers = 6371.0;
        private const double DegreesToRadians = Math.PI / 180.0;

        /// <summary>
        /// Calculates the distance in kilometers between two coordinates.
        /// </summary>
        /// <param name="origin">The origin coordinate.</param>
        /// <param name="destination">The destination coordinate.</param>
        /// <param name="decimalPlaces">The number of decimal places to round the result to (default is 1).</param>
        /// <returns>The distance in kilometers between the two coordinates.</returns>
        /// <exception cref="ArgumentException">Thrown when invalid coordinates are supplied.</exception>
        public static double GetDistanceInKilometers(this Coordinate origin, Coordinate destination, int decimalPlaces = 1)
        {
            if (!origin.ValidateCoordinates())
            {
                throw new ArgumentException("Invalid origin coordinates supplied.");
            }

            if (!destination.ValidateCoordinates())
            {
                throw new ArgumentException("Invalid destination coordinates supplied.");
            }

            var latitudeDifferenceRadians = DegreesToRadians * (destination.Latitude - origin.Latitude);
            var longitudeDifferenceRadians = DegreesToRadians * (destination.Longitude - origin.Longitude);

            var a = Math.Sin(latitudeDifferenceRadians / 2) * Math.Sin(latitudeDifferenceRadians / 2) +
                    Math.Cos(DegreesToRadians * origin.Latitude) * Math.Cos(DegreesToRadians * destination.Latitude) *
                    Math.Sin(longitudeDifferenceRadians / 2) * Math.Sin(longitudeDifferenceRadians / 2);

            return Math.Round(2 * EarthRadiusInKilometers * Math.Asin(Math.Min(1.0, Math.Sqrt(a))), decimalPlaces);
        }
    }
}
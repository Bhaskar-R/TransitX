using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TransitX.Common.Models
{
    /// <summary>
    /// Represents a vehicle used for transportation.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or sets the unique identifier of the vehicle.
        /// </summary>
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the vehicle.
        /// </summary>
        [BsonElement("type"), BsonRepresentation(BsonType.String)]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the capacity of the vehicle.
        /// </summary>
        [BsonElement("capacity"), BsonRepresentation(BsonType.Int32)]
        public int Capacity { get; set; }
    }
}

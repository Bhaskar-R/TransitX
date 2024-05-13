using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace TransitX.Common.Models
{
    /// <summary>
    /// Custom BSON serializer for the Coordinate class.
    /// </summary>
    public class CoordinateSerializer : SerializerBase<Coordinate>
    {
        /// <summary>
        /// Deserializes a BSON document to a Coordinate object.
        /// </summary>
        /// <param name="context">The deserialization context.</param>
        /// <param name="args">The deserialization arguments.</param>
        /// <returns>The deserialized Coordinate object.</returns>
        public override Coordinate Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            bsonReader.ReadStartDocument();
            var latitude = bsonReader.ReadDouble("Latitude");
            var longitude = bsonReader.ReadDouble("Longitude");
            var name = bsonReader.ReadString("Name");
            bsonReader.ReadEndDocument();

            return new Coordinate(latitude, longitude, name);
        }

        /// <summary>
        /// Serializes a Coordinate object to BSON.
        /// </summary>
        /// <param name="context">The serialization context.</param>
        /// <param name="args">The serialization arguments.</param>
        /// <param name="value">The Coordinate object to serialize.</param>
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Coordinate value)
        {
            var bsonWriter = context.Writer;
            bsonWriter.WriteStartDocument();
            bsonWriter.WriteDouble("Latitude", value.Latitude);
            bsonWriter.WriteDouble("Longitude", value.Longitude);
            bsonWriter.WriteString("Name", value.Name);
            bsonWriter.WriteEndDocument();
        }
    }
}

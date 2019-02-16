using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace camera_trigger_api_core.Models
{
    public class Trigger
    {
        public Trigger()
        {

        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("CameraName")]
        public string CameraName { get; set; }
        [BsonElement("TimeStamp")]
        public DateTime TimeStamp { get; set; }
    }
}

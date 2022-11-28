using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.Models;

public class BusinessUser : User
{
    [BsonElement]
    public string CVR { get; set; }
}

public class User
{
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ObjectId { get; set; }
    [BsonElement("UserName")]
    public string Name { get; set; }
    public string CPR { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Type { get; set; }
    public List<Reservation> Reservations { get; set; }
}

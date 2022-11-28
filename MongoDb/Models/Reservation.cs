namespace MongoDb.Models;

public class Reservation
{
    public string Name { get; set; }
    public string Timeslot { get; set; }
    public string Note { get; set;}
    public List<string> ParticipantCpr { get; set; }
}
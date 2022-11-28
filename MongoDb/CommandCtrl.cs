using MongoDB.Driver;
using MongoDb.Models;

namespace MongoDb;


public class CommandCtrl
{
    private UI.UI _ui;
    private IMongoCollection<Facility> _facilities;
    private readonly string connectionString = "mongodb://localhost:27017";
    private readonly string municipalityDB = "Municipalities";
    private readonly string facCollection = "Facilities";
    public CommandCtrl(UI.UI ui)
    {
        _ui = ui;
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(municipalityDB);
        _facilities = database.GetCollection<Facility>(facCollection);
    }

    public void GetFacilitiesOrderByKind()
    {

    }

    public void GetAllFacilitiesWAddress()
    {

    }

    public void GetReservations()
    {

    }

    public void GetReservationsWParticipants()
    {

    }

    public void GetMaintenanceHistory()
    {

    }
}
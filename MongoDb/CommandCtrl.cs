using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDb.Models;
using System.Net;

namespace MongoDb;


public class CommandCtrl
{
    private UI.UI _ui;
    private IMongoCollection<Facility> _facilities;
    private IMongoCollection<User> _users;
    private readonly string connectionString = "mongodb://localhost:27017";
    private readonly string municipalityDB = "Municipalities";
    private readonly string facCollection = "Facilities";
    private readonly string userCollection = "Users";
    public CommandCtrl(UI.UI ui)
    {
        _ui = ui;
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(municipalityDB);
        _facilities = database.GetCollection<Facility>(facCollection);
        _users = database.GetCollection<User>(userCollection);
    }

    public void GetFacilitiesOrderByKind()
    {
        _ui.Display("Getting a list of facilities ordered by kind\n");

        var found = _facilities.Find(_ => true).
            SortBy(f => f.Type).ToList();
        _ui.Display("Found " + found.Count + " entries\n\n");
        string output;
        string header = "Name                        Closest address                  Type\n";
        _ui.Display(header);
        foreach (var f in found)
        {
            output = "";
            output += f.Name;
            while (output.Length < header.Length - "Closest address                  Type".Length - 1)
            {
                output += " ";
            }

            output += f.ClosestAddress;
            while (output.Length < header.Length - "Type".Length - 1)
            {
                output += " ";
            }

            output += f.Type + "\n";
            _ui.Display(output);
        }
        _ui.Display("\n");
    }

    public void GetAllFacilitiesWAddress()
    {
        _ui.Display("Getting a list of facilities\n");
        var found = _facilities.Find(_ => true).ToList();
        _ui.Display("Found " + found.Count + " entries\n\n");
        string output;
        string header = "Name                        Closest address\n";
        _ui.Display(header);
        foreach (var f in found)
        {
            output = "";
            output += f.Name;
            while (output.Length < header.Length - "Closest Address".Length - 1)
            {
                output += " ";
            }

            output += f.ClosestAddress;
            output += "\n";
            _ui.Display(output);
        }
        _ui.Display("\n");
    }

    public void GetReservations()
    {
        string output;
        _ui.Display("Getting a list of reservations\n\n");
        var users = _users.Find(u => u.Reservations.Count > 0).ToList();
        string header = "Booking User                         Facility                        Date and time\n";
        _ui.Display(header);
        foreach (var u in users)
        {
            foreach (var r in u.Reservations)
            {
                output = "";
                output += u.CPR;
                while (output.Length < "Booking User                         ".Length)
                {
                    output += " ";
                }

                output += r.Name;
                
                while (output.Length < header.Length - "Date and time\n".Length)
                {
                    output += " ";
                }

                output += r.Timeslot;
                
                _ui.Display(output + "\n");
            }
        }
    }

    public void GetReservationsWParticipants()
    {
        string output;
        _ui.Display("Getting a list of reservations\n\n");
        var users = _users.Find(u => u.Reservations.Count > 0).ToList();
        string header = "Booking User                         Facility                        Date and time\n";
        _ui.Display(header);
        foreach (var u in users)
        {
            foreach (var r in u.Reservations)
            {
                output = "";
                output += u.CPR;
                while (output.Length < "Booking User                         ".Length)
                {
                    output += " ";
                }

                output += r.Name;
                
                while (output.Length < header.Length - "Date and time\n".Length)
                {
                    output += " ";
                }

                output += r.Timeslot;
                
                _ui.Display(output + "\n");
                _ui.Display("Participants: ");
                foreach (var cpr in r.ParticipantCpr)
                {
                    _ui.Display(cpr + ", ");
                }

                _ui.Display("\n");
            }
            
        }
    }

    public void GetMaintenanceHistory()
    {
        _ui.Display("Getting a list of facilities with maintenances\n");
        var found = _facilities.Find(f => f.Minterventions.Count > 0).ToList();
        _ui.Display("Found " + found.Count + " entries\n\n");
        string header = "Technician Name         Facility Name        Date\n";
        string output;
        foreach (var f in found)
        {
            foreach (var m in f.Minterventions)
            {
                output = "";
                output += m.Name;
                while (output.Length < "Technician Name         ".Length)
                {
                    output += " ";
                }

                output += f.Name;
                while (output.Length < "Technician Name          Facility Name        ".Length)
                {
                    output += " ";
                }

                output += m.Date;
                output += "\n";
                _ui.Display(output);
            }
        }
        _ui.Display("\n");
    }
}
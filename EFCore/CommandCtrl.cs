using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using EFCore.Controllers;
using EFCore.data;
using EFCore.Model;
using EFCore.UI;
using Microsoft.EntityFrameworkCore;

namespace EFCore;

public class CommandCtrl
{
    private Ui _ui;
    private FacilityController _facilityController;
    private ReservationController _reservationController;
    private UserController _userController;
    private Context _context;

    public CommandCtrl(Ui ui)
    {
        _ui = ui;
        _context = new Context();
        _facilityController = new FacilityController(_context);
        _reservationController = new ReservationController(_context);
        _userController = new UserController(_context);
        if (!ContextContainsData())
        {
            SeedDummyData();
        }
    }
    
    public void GetAllFacilitiesWAddress()
    {
        var facilities = _context.Facilities.ToList();
        string formatted = "";
        foreach (Facility f in facilities)
        {
            string line = "";
            line += "Name: " + f.FacName;
            while (line.Length < 40)
            {
                line += ' ';
            }

            line += "Lat: " + String.Format("{0:0.000}", f.GPS_lat)  + "  ";
            line += "Lon: " + String.Format("{0:0.000}", f.GPS_lon);
            formatted += line + "\n";
        }
        _ui.Display(formatted);
    }
    public void GetReservations()
    {
        List<Reservation> reservations = _context.Reservations.ToList();
      
        foreach (var r in reservations)
        {
            _context.Entry(r).Reference(res => res.Facility).Load();
            _context.Entry(r).Reference(res => res.User).Load();
            _context.Entry(r).Collection(r => r.Participants)
                .Load();
            string formatted = "";
    
            string line = "";
            line += "Fac Name: " + r.Facility.FacName;
            while (line.Length < 30) {
                    line += ' ';
            } 
            line += "Username: " + r.User.Name;
            while (line.Length < 50)
            { 
                line += ' ';
            }
            line += "Time start: " + r.Start + " end: " + r.End;
            formatted += line + "\n";
                        
            _ui.Display(formatted); 
        }
    }

    public void GetReservationsWParticipants()
    {
        List<Reservation> reservations = _context.Reservations.ToList();

        foreach (var r in reservations)
        {
            _context.Entry(r).Reference(res => res.Facility).Load();
            _context.Entry(r).Reference(res => res.User).Load();
            _context.Entry(r).Collection(r => r.Participants)
                .Load();
            string formatted = "";

            string line = "";
            line += "Fac Name: " + r.Facility.FacName;
            while (line.Length < 30)
            {
                line += ' ';
            }
            
            line += "Time start: " + r.Start + " end: " + r.End;
            formatted += line + "\n";

            _ui.Display(formatted);
            formatted = "Participants: \n";
            foreach (var p in r.Participants)
            {
                formatted += "CPR: " + p.CPRNumber + "\n";
            }
            _ui.Display(formatted);
        }
    }


    public void GetFacilitiesOrderByKind()
    {
        List<Facility> facilities = _context.Facilities.
            OrderBy(f => f.FacType)
            .ToList();
        string formatted = "";
        foreach (Facility f in facilities)
        {
            string line = "";
            line += "Kind: " + f.FacType;
            while (line.Length < 20)
            {
                line += ' ';
            }
            line += "Name: " + f.FacName;
            while (line.Length < 64)
            { 
                line += ' ';
            }
            line += "Lat: " + String.Format("{0:0.000}", f.GPS_lat) + "  ";
            line += "Lon: " + String.Format("{0:0.000}", f.GPS_lon);
            formatted += line + "\n";
        }
        _ui.Display(formatted);
    }

    private bool ContextContainsData()
    {
        var f = _context.Facilities.FirstOrDefault();
        var r = _context.Reservations.FirstOrDefault();
        var u = _context.Users.FirstOrDefault();
        if (f != null || r != null || u != null)
        {
            return true;
        }
        return false;
    }

    // Useful for testing
    public void DeleteAllData()
    {
        var f = _facilityController.GetAll();
        var r = _reservationController.GetAll();
        var u = _userController.GetAll();
        var m = _context.MaintenanceInterventions.ToList();
        var p = _context.Participants.ToList();
        foreach (var facility in f)
        {
            _facilityController.Delete(facility.Id);
        }

        foreach (var par in p)
        {
            _context.Participants.Remove(par);
            _context.SaveChanges();
        }
        
        foreach (var maint in m)
        {
            _context.MaintenanceInterventions.Remove(maint);
            _context.SaveChanges();
        }

        foreach (var reservation in r)
        {
            _reservationController.Delete(reservation.Id);
        }
        foreach (var user in u)
        {
            _userController.Delete(user.CPRNumber);
        }
    }

    public void SeedDummyData()
    {
        Facility f1 = new Facility()
        {
            GPS_lat = 20.01,
            GPS_lon = 50.31,
            FacType = "Playground",
            FacItems = "Bench",
            FacName = "The fun playground",
            FacRules = "No fire",
        };

        Facility f2 = new Facility()
        {
            GPS_lat = 50.01,
            GPS_lon = 121.3123,
            FacType = "Shelter",
            FacItems = "Fire pit",
            FacName = "Shelter by Aarhus",
            FacRules = "Only one overnight stay"
        };
        Facility f3 = new Facility()
        {
            GPS_lat = 42.02,
            GPS_lon = 55.451,
            FacType = "Playground",
            FacItems = "Toilets",
            FacName = "The playground at Main Street",
            FacRules = "No fire"
        };
        Facility f4 = new Facility()
        {
            GPS_lat = 68.0131,
            GPS_lon = 13.2314,
            FacType = "Tennis court",
            FacItems = "Balls",
            FacName = "The land of tennis",
            FacRules = "No food"
        };
        _facilityController.Add(f1);
        _facilityController.Add(f2);
        _facilityController.Add(f3);
        _facilityController.Add(f4);

        User u1 = new User()
        {
            CPRNumber = 1111200011,
            Email = "simon@live.com",
            Name = "Simon",
            PhoneNumber = 22334455,
            CVR = 1234
        };
        User u2 = new User()
        {
            CPRNumber = 2202952020,
            Email = "sara@live.com",
            Name = "Sara",
            PhoneNumber = 21233435,
            CVR = 5678
        };
        User u3 = new User()
        {
            CPRNumber = 2010900135,
            Email = "rasmus@live.com",
            Name = "Rasmus",
            PhoneNumber = 92384961,
            CVR = 9012
        };
        _userController.Add(u1);
        _userController.Add(u2);
        _userController.Add(u3);

        Reservation r1 = new Reservation()
        {
            Start = new DateTime(2022, 2, 7, 12, 00, 00),
            End = new DateTime(2022, 2, 7, 14, 00, 00),
            User = u1,
            Facility = f1,
        };
        
        Reservation r2 = new Reservation()
        {
            Start = new DateTime(2022, 4, 5, 10, 30, 00),
            End = new DateTime(2022, 4, 5, 12, 15, 00),
            User = u3,
            Facility = f4
        };
        _context.Reservations.Add(r1);
        _context.Reservations.Add(r2);
        _context.SaveChanges();
        
        long cpr =  1941491231;
        long cpr2 = 2020200222;
        long cpr3 = 3004894242;
        var r = _context.Reservations.FirstOrDefault(res => res.Facility == r1.Facility);
        _reservationController.AddParticipant((cpr), r.Id);
        r = _context.Reservations.FirstOrDefault(res => res.Facility == r2.Facility);
        _reservationController.AddParticipant((cpr), r.Id);
        _reservationController.AddParticipant(cpr2, r.Id);
        _reservationController.AddParticipant(cpr3, r.Id);
        _context.SaveChanges();
        BusinessUser b1 = new BusinessUser()
        {
            CPRNumber = 1234567890,
            Email = "Jysk@jysk.dk",
            PhoneNumber = 12345678,
            BusinessCVR = 12345678,
            Name = "Jysk Sengetøjslager",
            Reservations = new List<Reservation>()
        };
        BusinessUser b2 = new BusinessUser()
        {
            CPRNumber = 98765432,
            Email = "McD@mcd.dk",
            PhoneNumber = 12345678,
            BusinessCVR = 12345678,
            Name = "MaCDonalds",
        };
        
        
        BusinessUser b3 = new BusinessUser()
        {
            CPRNumber = 75645279,
            Email = "Harald Nyborg@hn.dk",
            PhoneNumber = 12345678,
            BusinessCVR = 12345678,
            Name = "Harald Nyborg",
        };
        try
        {
            _userController.Add(b1);
            _userController.Add(b2);
            _userController.Add(b3);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Data keys" + e.Data.Keys.GetType() + " val:" + e.Data.Values);
        }


        MaintenanceIntervention m1 = new MaintenanceIntervention()
        {
            Facility = f1,
            StartDate = new DateTime(2020, 4, 10),
            TechnicianName = "Thomas Wayne"
        };
        MaintenanceIntervention m2 = new MaintenanceIntervention()
        {
            Facility = f2,
            StartDate = new DateTime(2022, 7, 12),
            TechnicianName = "Bob More"
        };
        MaintenanceIntervention m3 = new MaintenanceIntervention()
        {
            Facility = f4,
            StartDate = new DateTime(2022, 1, 1),
            TechnicianName = "Dash Wild"
        };
       
        _context.MaintenanceInterventions.Add(m1);
        _context.MaintenanceInterventions.Add(m2);
        _context.MaintenanceInterventions.Add(m3);
        _context.SaveChanges();
    }

    public void GetMaintenanceHistory()
    {
        List<MaintenanceIntervention> maint =
            _context.MaintenanceInterventions.Include(m => m.Facility)
                .OrderBy(m => m.StartDate)
                .ToList();

        string formatted = "Id  Technician Name    Facility Name        Date\n";
        foreach (var m in maint)
        {
            string line = String.Format("{0:0}",m.Id);
            while (line.Length < "Id  ".Length)
            {
                line += ' ';
            }
            line += m.TechnicianName;
            while (line.Length < "Id  Technician Name    ".Length)
            {
                line += ' ';
            }

            line += m.Facility.FacName;
            while (line.Length < "Id  Technician Name     Facility Name       ".Length)
            {
                line += ' ';
            }

            line += m.StartDate;
            formatted += line + "\n";
        }
        _ui.Display(formatted);
    }
}
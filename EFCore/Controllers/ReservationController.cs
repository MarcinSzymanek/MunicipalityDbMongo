using EFCore.data;
using EFCore.Model;
using Microsoft.EntityFrameworkCore;


namespace EFCore.Controllers;

public class ReservationController : IController<Reservation>
{
    private readonly Context _context;

    public ReservationController(Context context)
    {
        _context = context;
    }

    public Reservation Get(long id)
    {
        throw new NotImplementedException();
    }

    public void AddParticipant(long cpr, int ReservationId)
    {
        if (_context.Participants.FirstOrDefault(p => p.CPRNumber == cpr) == null)
        {
            var p = _context.Participants.Add(new Participant()
                { CPRNumber = cpr });
            _context.SaveChanges();
        }

        var toAdd = _context.Participants.FirstOrDefault(p => p.CPRNumber == cpr);
        var r = _context.Reservations.Include(res => res.Participants).FirstOrDefault(r => r.Id == ReservationId);
        r.Participants.Add(toAdd);
    }

    public List<Reservation> GetAll()
    {
        List<Reservation> reservations = _context.Reservations.ToList();
        return reservations;
    }

    public bool Add(Reservation reservation)
    {
        try
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
        return true;
    }

    public bool Delete(long id)
    {
        var r = _context.Reservations.Find((int)id);
        if (r != null)
        {
            _context.Reservations.Remove(r);
            _context.SaveChanges();
            return true;
        }

        return false;
    }
}

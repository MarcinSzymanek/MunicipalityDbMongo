using System.Linq.Expressions;
using EFCore.data;
using EFCore.Model;
using GeoCoordinatePortable;


namespace EFCore.Controllers;

public class FacilityController : IController<Facility>
{
    private readonly Context _context;

    public FacilityController(Context context)
    {
        _context = context;
    }

    public Facility? Get(long id)
    {
        throw new NotImplementedException();
    }

    public List<Facility> GetAll()
    {
        List<Facility> facilities = _context.Facilities.ToList();
        return facilities;
    }

    public bool Add(Facility facility)
    {
        try
        {
            GeoCoordinate check = new GeoCoordinate(facility.GPS_lat, facility.GPS_lon);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
        try
        {
            _context.Facilities.Add(facility);
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
        var f = _context.Facilities.Find((int)id);
        if (f != null)
        {
            _context.Facilities.Remove(f);
            _context.SaveChanges();
            return true;
        }

        return false;
    }
}
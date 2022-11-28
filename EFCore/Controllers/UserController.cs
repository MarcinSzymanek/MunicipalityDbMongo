using EFCore.data;
using EFCore.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Controllers;

public class UserController : IController<User>
{
    private Context _context;

    public UserController(Context context)
    {
        _context = context;
    }

    public bool Add(User entity)
    {
        _context.Users.Add(entity);
        _context.SaveChanges();
        return true;
    }

    public bool Delete(long id)
    {
        User? user = _context.Users.Find(id);
        if (user == null)
        {
            return false;
        }
        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }

    public User? Get(long id)
    {
        return _context.Users.Find(id);
    }

    public List<User> GetAll()
    {
        return _context.Users.ToList();
    }
}

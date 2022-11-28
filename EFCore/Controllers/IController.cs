namespace EFCore.Controllers;

public interface IController<T>
{
    T? Get(long id);
    List<T> GetAll();
    bool Add(T entity);
    bool Delete(long id);
}
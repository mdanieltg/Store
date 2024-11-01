namespace Store.WebAPI.DataAccess.Repositories;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    ValueTask<T?> Get(Guid id);
    ValueTask Update(Guid id, T updatedItem);
    ValueTask Delete(T item);
}

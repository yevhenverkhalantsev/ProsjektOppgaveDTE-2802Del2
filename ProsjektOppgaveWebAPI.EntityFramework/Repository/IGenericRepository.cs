namespace ProsjektOppgaveWebAPI.EntityFramework.Repository;

public interface IGenericRepository<T> where T : class
{
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    
    Task<T> GetById(int id);
    IQueryable<T> GetAll();
}
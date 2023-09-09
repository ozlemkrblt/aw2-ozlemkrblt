using Vk.Base;

namespace Vk.Data.Repository;

public interface IGenericRepository<TEntity> where TEntity : BaseModel
{
    TEntity GetById(int id);
    List<TEntity> GetAll();
    void Delete(int id);
    void Delete(TEntity entity);
    void Remove(int id);
    void Remove(TEntity entity);
    void Update(TEntity entity);
    void Insert(TEntity entity);
    void InsertRange(List<TEntity> entities);
    IQueryable<TEntity> GetAsQueryable();
}

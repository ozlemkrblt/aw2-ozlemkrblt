using Microsoft.EntityFrameworkCore;
using Vk.Base;
using Vk.Data.Context;

namespace Vk.Data.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel
{
    private readonly VkDbContext dbContext;

    public GenericRepository(VkDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Delete(TEntity entity)
    {
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        entity.UpdateUserId = 1;
        dbContext.Set<TEntity>().Update(entity);
    }


    public void Delete(int id)
    {
        var entity = dbContext.Set<TEntity>().Find(id);
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        entity.UpdateUserId = 1;
        dbContext.Set<TEntity>().Update(entity);
    }

    public List<TEntity> GetAll()
    {
        return  dbContext.Set<TEntity>().AsNoTracking().ToList();
    }

    public IQueryable<TEntity> GetAsQueryable()
    {
        return dbContext.Set<TEntity>().AsQueryable();
    }

    public TEntity GetById(int id)
    {
        return dbContext.Set<TEntity>().Find(id);
    }

    public void Insert(TEntity entity)
    {
        entity.InsertDate = DateTime.UtcNow;
        entity.InsertUserId = 1;
        dbContext.Set<TEntity>().Add(entity);
    }

    public void InsertRange(List<TEntity> entities)
    {
        entities.ForEach(x =>
        {
            x.InsertUserId = 1;
            x.InsertDate = DateTime.UtcNow; 
        });
        dbContext.Set<TEntity>().AddRange(entities);
    }

    public void Remove(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    public void Remove(int id)
    {
        var entity = dbContext.Set<TEntity>().Find(id);
        dbContext.Set<TEntity>().Remove(entity);
    }

    public void Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
    }
   
}

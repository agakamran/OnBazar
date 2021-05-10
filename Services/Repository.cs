using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnBazar.Data;
using System.Transactions;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace OnBazar.Services
{
    public interface IRepository<TEntity>
    {
        //Task<TEntity> Find(string key);
        //Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll();
        Task EditAsync(TEntity entity);
        Task EditAsync1(TEntity []entity);
        Task InsertAsync(TEntity entity);
        Task _InsertAsync(TEntity[] entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync1(TEntity[] entity);
    }
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal ApplicationDbContext _dbContext;
        internal DbSet<TEntity> DbSet;
        public Repository(ApplicationDbContext context)
        {
            _dbContext = context;
            DbSet = context.Set<TEntity>();
        }
        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }      

        public IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public async Task EditAsync(TEntity entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception xx)
            {
                Console.WriteLine(xx + "deyishimde sehv");
            }
        }
        public async Task EditAsync1(TEntity[] entity)
        {
            try
            {
                foreach (var v in entity)
                {
                    _dbContext.Entry(v).State = EntityState.Modified;
                }
                await _dbContext.SaveChangesAsync();               
            }
            catch (Exception xx)
            {
                Console.WriteLine(xx + "deyishimde sehv");
            }
        }
        public async Task InsertAsync(TEntity entity)
        {
            try
            {
                // var container = await DbSet.Containers.FirstOrDefaultAsync(
                // c => c. == item.ContainerId );
                DbSet.Add(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message + "insertde sehv");
            }
            
        }
       /* public async Task SaveNewItem([FromBody] Item item)
        {
            var container = await db.Containers.FirstOrDefaultAsync(
            c => c.Id == item.ContainerId
            );
            if (container == null) return false;
            db.Items.Add(item);
            await db.SaveChangesAsync();
            return true;
        }*/

        public async Task _InsertAsync(TEntity[] entity)
        {
            try
            {
                foreach(var v in entity)
                {
                    DbSet.Add(v);                   
                }
                await _dbContext.SaveChangesAsync();  
                
            }
            catch (Exception xx)
            {
                Console.WriteLine(xx + "insertde sehv");
            }
        }
        public async Task DeleteAsync(TEntity entity)
        {
            try
            {
                DbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception r)
            {

            }
        }

        public async Task DeleteAsync1(TEntity[] entity)
        {
            try
            {
                foreach(var v in entity)
                {
                    DbSet.Remove(v);
                }                
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception r)
            {

            }
        }

        
    }
}

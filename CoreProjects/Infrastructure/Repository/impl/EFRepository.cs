using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCoreProject.Models;

namespace Infrastructure.Repository.impl
{
    public class EFRepository<T> : IRepository<T> where T : class, new()
    {
        protected readonly WebCoreProjectContext _dbContext;

        public EFRepository(WebCoreProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> All()
        {
            return _dbContext.Set<T>();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task<T> AddAsync(T entity, bool isSaveChange = true)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            if (isSaveChange)
                await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity, bool isSaveChange = true)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            if (isSaveChange)
                await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity, bool isSaveChange = true)
        {
            _dbContext.Set<T>().Remove(entity);
            if (isSaveChange)
                await _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveChange()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// List<MySqlParameter> param = new List<MySqlParameter> { };
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> SqlQuery(string sql, params object[] parameters)
        {
            return await _dbContext.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();
        }

        public async Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }

    }
}

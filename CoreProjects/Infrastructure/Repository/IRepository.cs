using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepository<T> where T : class, new()
    {
        IQueryable<T> All();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<T> AddAsync(T entity, bool isSaveChange = true);
        Task UpdateAsync(T entity, bool isSaveChange = true);
        Task DeleteAsync(T entity, bool isSaveChange = true);

        Task<int> SaveChange();

        /// <summary>
        /// List<MySqlParameter> param = new List<MySqlParameter> { };
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> SqlQuery(string sql, params object[] parameters);

        Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters);
    }
}

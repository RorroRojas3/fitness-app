using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Repository.Pattern.Interface
{
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        ///     Gets all T from DB
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAll();

        /// <summary>
        ///     Gets single T based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> Get(Guid id);

        /// <summary>
        ///     Gets T with LINQ expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> GetWithExpression(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Gets list of T with LINQ expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<T>> GetAllWithExpression(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Adds T to DB
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> Add(T entity);

        /// <summary>
        ///     Updates T on DB
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> Update(T entity);

        /// <summary>
        ///     Deletes T on DB based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid id);
    }
}
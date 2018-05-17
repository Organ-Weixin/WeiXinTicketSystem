using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Models.PageList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Repository
{
    /// <summary>
    /// 查询方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryQueryable<T> where T : EntityBase
    {
        IRepositoryQueryable<T> And(Expression<Func<T, bool>> expression);
        IRepositoryQueryable<T> GroupBy(Expression<Func<T, object>> expression);
        IRepositoryQueryable<T2> Join<T2>(Expression<Func<T, T2, bool>> expression, string joinTableAliasName = null) where T2 : EntityBase;
        IRepositoryQueryable<T2> LeftJoin<T2>(Expression<Func<T, T2, bool>> expression, string joinTableAliasName = null) where T2 : EntityBase;
        IRepositoryQueryable<T> Or(Expression<Func<T, bool>> expression);
        IRepositoryQueryable<T> OrderBy(Expression<Func<T, object>> expression);
        IRepositoryQueryable<T> OrderByDescending(Expression<Func<T, object>> expression);
        IRepositoryQueryable<T> Select(params Expression<Func<T, object>>[] expressions);
        IRepositoryQueryable<T> Select(string tableAliasName, params Expression<Func<T, object>>[] expressions);
        IRepositoryQueryable<T> SelectAverage(Expression<Func<T, object>> expression);
        IRepositoryQueryable<T> SelectCount(Expression<Func<T, object>> expression);
        IRepositoryQueryable<T> SelectDistinct(Expression<Func<T, object>> expression);
        IRepositoryQueryable<T> SelectMax(Expression<Func<T, object>> expression);
        IRepositoryQueryable<T> SelectMin(Expression<Func<T, object>> expression);
        IRepositoryQueryable<T> SelectSum(Expression<Func<T, object>> expression);
        IRepositoryQueryable<T> Skip(int count);
        IRepositoryQueryable<T> Take(int count);
        IRepositoryQueryable<T> Where(Expression<Func<T, bool>> expression);
        IRepositoryQueryable<T> WhereIsIn(Expression<Func<T, object>> expression, IEnumerable<object> values);
        IRepositoryQueryable<T> WhereNotIn(Expression<Func<T, object>> expression, IEnumerable<object> values);

        IList<T> ToList();
        Task<IList<T>> ToListAsync();
        IList<T> ToListWithTransaction(IDbConnection connection, IDbTransaction transaction = null);
        Task<IList<T>> ToListWithTransactionAsync(IDbConnection connection, IDbTransaction transaction = null);

        Task<IPageList<T>> ToPageListAsync();
        Task<IPageList<T>> ToPageListWithTransactionAsync(IDbConnection connection, IDbTransaction transaction = null);

        T SingleOrDefault();
        Task<T> SingleOrDefaultAsync();
        T SingleOrDefaultWithTransaction(IDbConnection connection, IDbTransaction transaction = null);
        Task<T> SingleOrDefaultWithTransactionAsync(IDbConnection connection, IDbTransaction transaction = null);

        int Count();
        Task<int> CountAsync();
        int CountWithTransaction(IDbConnection connection, IDbTransaction transaction = null);
        Task<int> CountAsyncWithTransaction(IDbConnection connection, IDbTransaction transaction = null);
    }
}

using WeiXinTicketSystem.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Models.PageList.Impl;

namespace WeiXinTicketSystem.Entity.Repository
{
    /// <summary>
    /// 数据仓库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// 查询
        /// </summary>
        IRepositoryQueryable<T> Query { get; }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(T entity);
        /// <summary>
        /// 添加（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertAsync(T entity);
        /// <summary>
        /// 添加（可包含事务，同步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        int InsertWithTransaction(T entity, IDbConnection connection, IDbTransaction transaction = null);
        /// <summary>
        /// 添加（可包含事务，异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<int> InsertWithTransactionAsync(T entity, IDbConnection connection, IDbTransaction transaction = null);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// 更新（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);
        /// <summary>
        /// 更新（可包含事务，同步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        void UpdateWithTransaction(T entity, IDbConnection connection, IDbTransaction transaction = null);
        /// <summary>
        /// 更新（可包含事务，异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task UpdateWithTransactionAsync(T entity, IDbConnection connection, IDbTransaction transaction = null);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// 删除异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);
        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        int Execute(string sql, object param = null, CommandType? commandType = default(CommandType?));
        /// <summary>
        /// 执行sql（异步）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task ExecuteAsync(string sql, object param = null);
        /// <summary>
        /// 执行sql（可包含事务）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task ExecuteWithTransactionAsync(IDbConnection connection, string sql, object param = null,
            IDbTransaction transaction = null);

        /// <summary>
        /// 查询sql（异步）
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<IEnumerable<TQuery>> QueryAsync1<TQuery>(string sql, object param = null, CommandType? commandType = null);
        /// <summary>
        /// 查询sql（异步）
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<IList<TQuery>> QueryAsync<TQuery>(string sql, IDictionary<string, object> param = null, CommandType? commandType = null);
        /// <summary>
        /// 查询sql(分页异步)
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<IPageList<TQuery>> QueryPageListAsync<TQuery>(string sql, IDictionary<string,object> param = null, CommandType? commandType = null);
        /// <summary>
        /// 查询sql(同步)
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IEnumerable<TQuery> QuerySync<TQuery>(string sql, object param = null, CommandType? commandType = null);
        /// <summary>
        /// 查询sql（异步，可包含事务）
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<IEnumerable<TQuery>> QueryWithTransactionAsync<TQuery>(IDbConnection connection, string sql,
            object param = null, IDbTransaction transaction = null, CommandType? commandType = null);

        /// <summary>
        /// 同时查询两张表，返回内容以“Id”字段分隔
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IEnumerable<TReturn> QueryDouble<TFirst, TSecond, TReturn>(string sql,
            Func<TFirst, TSecond, TReturn> map, object param = null, CommandType? commandType = default(CommandType?));

        /// <summary>
        /// 批量合并
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="newSource"></param>
        /// <param name="newKeySelector"></param>
        /// <param name="oldSource"></param>
        /// <param name="oldKeySelector"></param>
        void BulkMerge<TKey>(IEnumerable<T> newSource,
          Func<T, TKey> newKeySelector,
          IEnumerable<T> oldSource,
          Func<T, TKey> oldKeySelector);

        /// <summary>
        /// 批量合并(外部事务)
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="newSource"></param>
        /// <param name="newKeySelector"></param>
        /// <param name="oldSource"></param>
        /// <param name="oldKeySelector"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        void BulkMerge<TKey>(IEnumerable<T> newSource,
          Func<T, TKey> newKeySelector,
          IEnumerable<T> oldSource,
          Func<T, TKey> oldKeySelector,
          IDbConnection connection,
          IDbTransaction transaction);
    }
}

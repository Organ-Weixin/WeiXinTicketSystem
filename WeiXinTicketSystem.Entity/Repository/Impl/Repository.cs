using Dapper;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Models.PageList.Impl;

namespace WeiXinTicketSystem.Entity.Repository.Impl
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        public IRepositoryQueryable<T> Query
        {
            get
            {
                return new RepositoryQueryable<T>();
            }
        }

        public int Insert(T entity)
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                return connection.Insert<int>(entityToInsert: entity);
            }
        }

        public async Task<int> InsertAsync(T entity)
        {
            using (var connection = await DbConnectionFactory.OpenConnectionAsync())
            {
                return await connection.InsertAsync<int>(entityToInsert: entity);
            }
        }

        public int InsertWithTransaction(T entity, IDbConnection connection, IDbTransaction transaction = null)
        {
            return connection.Insert<int>(entityToInsert: entity, transaction: transaction);
        }

        public async Task<int> InsertWithTransactionAsync(T entity, IDbConnection connection, IDbTransaction transaction = null)
        {
            return await connection.InsertAsync<int>(entityToInsert: entity, transaction: transaction);
        }

        public void Update(T entity)
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                connection.Update(entityToUpdate: entity);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            using (var connection = await DbConnectionFactory.OpenConnectionAsync())
            {
                await connection.UpdateAsync(entityToUpdate: entity);
            }
        }

        public void UpdateWithTransaction(T entity, IDbConnection connection, IDbTransaction transaction = null)
        {
            connection.Update(entityToUpdate: entity, transaction: transaction);
        }

        public async Task UpdateWithTransactionAsync(T entity, IDbConnection connection, IDbTransaction transaction = null)
        {
            await connection.UpdateAsync(entityToUpdate: entity, transaction: transaction);
        }

        public void Delete(T entity)
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                connection.Delete(entityToDelete: entity);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            using (var connection = await DbConnectionFactory.OpenConnectionAsync())
            {
                await connection.DeleteAsync(entityToDelete: entity);
            }
        }

        public int Execute(string sql, object param = null, CommandType? commandType = default(CommandType?))
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                return connection.Execute(sql, param: param, commandType: commandType);
            }
        }

        public async Task ExecuteAsync(string sql, object param = null)
        {
            using (var connection = await DbConnectionFactory.OpenConnectionAsync())
            {
                await connection.ExecuteAsync(sql, param: param);
            }
        }

        public async Task ExecuteWithTransactionAsync(IDbConnection connection, string sql, object param = null,
            IDbTransaction transaction = null)
        {
            await connection.ExecuteAsync(sql, param: param, transaction: transaction);
        }

        public async Task<IPageList<TQuery>> QueryPageListAsync<TQuery>(string sql, IDictionary<string, object> param = null, CommandType? commandType = null)
        {
            using (var connection = await DbConnectionFactory.OpenConnectionAsync())
            {
                var reader = await connection.QueryMultipleAsync(sql, param, commandType: CommandType.Text);
                var list = await reader.ReadAsync<TQuery>();
                var total = (await reader.ReadAsync<int>()).Single();

                return new PageList<TQuery>(list, -1, -1, total);
            }
        }
        public async Task<IList<TQuery>> QueryAsync<TQuery>(string sql, IDictionary<string, object> param = null, CommandType? commandType = null)
        {
            using (var connection = await DbConnectionFactory.OpenConnectionAsync())
            {
                var task = await connection.QueryAsync<TQuery>(sql, param, commandType: CommandType.Text);
                return task.ToList();
            }
        }

        public async Task<IEnumerable<TQuery>> QueryAsync1<TQuery>(string sql, object param = null, CommandType? commandType = null)
        {
            using (var connection = await DbConnectionFactory.OpenConnectionAsync())
            {
                return await connection.QueryAsync<TQuery>(sql, param: param, commandType: commandType);
            }
        }

        public IEnumerable<TQuery> QuerySync<TQuery>(string sql, object param = null, CommandType? commandType = null)
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                return connection.Query<TQuery>(sql, param: param, commandType: commandType);
            }
        }

        public async Task<IEnumerable<TQuery>> QueryWithTransactionAsync<TQuery>(IDbConnection connection, string sql,
            object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return await connection.QueryAsync<TQuery>(sql, param: param, transaction: transaction, commandType: commandType);
        }

        public IEnumerable<TReturn> QueryDouble<TFirst, TSecond, TReturn>(string sql,
            Func<TFirst, TSecond, TReturn> map, object param = null, CommandType? commandType = default(CommandType?))
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                return connection.Query(sql, map, param, commandType: commandType);
            }
        }

        public void BulkMerge<TKey>(IEnumerable<T> newSource,
          Func<T, TKey> newKeySelector,
          IEnumerable<T> oldSource,
          Func<T, TKey> oldKeySelector)
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                var groups = oldSource.FullOuterJoin(newSource,
                    oldKeySelector,
                    newKeySelector,
                    (oldItem, newItem, key) => new { oldItem, newItem });

                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var group in groups)
                        {
                            if (group.oldItem == null) // add new item to database
                            {
                                connection.Insert(group.newItem, trans);
                            }
                            else if (group.newItem == null) // delete old item 
                            {
                                connection.Delete(group.oldItem, trans);
                            }
                            else // update old item
                            {
                                connection.Update(group.newItem, trans);
                            }
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public void BulkMerge<TKey>(IEnumerable<T> newSource,
          Func<T, TKey> newKeySelector,
          IEnumerable<T> oldSource,
          Func<T, TKey> oldKeySelector,
          IDbConnection connection,
          IDbTransaction transaction)
        {
            var groups = oldSource.FullOuterJoin(newSource,
                oldKeySelector,
                newKeySelector,
                (oldItem, newItem, key) => new { oldItem, newItem });

            foreach (var group in groups)
            {
                if (group.oldItem == null) // add new item to database
                {
                    connection.Insert(group.newItem, transaction);
                }
                else if (group.newItem == null) // delete old item 
                {
                    connection.Delete(group.oldItem, transaction);
                }
                else // update old item
                {
                    connection.Update(group.newItem, transaction);
                }
            }

        }
    }
}

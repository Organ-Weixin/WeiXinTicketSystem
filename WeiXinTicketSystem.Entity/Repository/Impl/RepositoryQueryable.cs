using Dapper;
using LambdaSqlBuilder;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Models.PageList.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Repository.Impl
{
    public class RepositoryQueryable<T> : IRepositoryQueryable<T> where T : EntityBase
    {
        private SqlLam<T> _query;

        public RepositoryQueryable()
        {
            _query = new SqlLam<T>();
        }

        private RepositoryQueryable(SqlLam<T> query)
        {
            _query = query;
        }

        public IRepositoryQueryable<T> And(Expression<Func<T, bool>> expression)
        {
            _query.And(expression);
            return this;
        }

        public IRepositoryQueryable<T> GroupBy(Expression<Func<T, object>> expression)
        {
            _query.GroupBy(expression);
            return this;
        }

        public IRepositoryQueryable<T2> Join<T2>(Expression<Func<T, T2, bool>> expression, string joinTableAliasName = null) where T2 : EntityBase
        {
            var joinQuery = _query.Join(expression, joinTableAliasName);
            return new RepositoryQueryable<T2>(joinQuery);
        }

        public IRepositoryQueryable<T2> LeftJoin<T2>(Expression<Func<T, T2, bool>> expression, string joinTableAliasName = null) where T2 : EntityBase
        {
            var joinQuery = _query.LeftJoin(expression, joinTableAliasName);
            return new RepositoryQueryable<T2>(joinQuery);
        }

        public IRepositoryQueryable<T> Or(Expression<Func<T, bool>> expression)
        {
            _query.Or(expression);
            return this;
        }

        public IRepositoryQueryable<T> OrderBy(Expression<Func<T, object>> expression)
        {
            _query.OrderBy(expression);
            return this;
        }

        public IRepositoryQueryable<T> OrderByDescending(Expression<Func<T, object>> expression)
        {
            _query.OrderByDescending(expression);
            return this;
        }

        public IRepositoryQueryable<T> Select(params Expression<Func<T, object>>[] expressions)
        {
            _query.Select(expressions);
            return this;
        }

        public IRepositoryQueryable<T> Select(string tableAliasName, params Expression<Func<T, object>>[] expressions)
        {
            _query.Select(tableAliasName, expressions);
            return this;
        }

        public IRepositoryQueryable<T> SelectAverage(Expression<Func<T, object>> expression)
        {
            _query.SelectAverage(expression);
            return this;
        }

        public IRepositoryQueryable<T> SelectCount(Expression<Func<T, object>> expression)
        {
            _query.SelectCount(expression);
            return this;
        }

        public IRepositoryQueryable<T> SelectDistinct(Expression<Func<T, object>> expression)
        {
            _query.SelectDistinct(expression);
            return this;
        }

        public IRepositoryQueryable<T> SelectMax(Expression<Func<T, object>> expression)
        {
            _query.SelectMax(expression);
            return this;
        }

        public IRepositoryQueryable<T> SelectMin(Expression<Func<T, object>> expression)
        {
            _query.SelectMin(expression);
            return this;
        }

        public IRepositoryQueryable<T> SelectSum(Expression<Func<T, object>> expression)
        {
            _query.SelectSum(expression);
            return this;
        }

        public IRepositoryQueryable<T> Skip(int count)
        {
            _query.Skip(count);
            return this;
        }

        public IRepositoryQueryable<T> Take(int count)
        {
            _query.Take(count);
            return this;
        }

        public IRepositoryQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            _query.Where(expression);
            return this;
        }


        public IRepositoryQueryable<T> WhereIsIn(Expression<Func<T, object>> expression, IEnumerable<object> values)
        {
            _query.WhereIsIn(expression, values);
            return this;
        }

        public IRepositoryQueryable<T> WhereNotIn(Expression<Func<T, object>> expression, IEnumerable<object> values)
        {
            _query.WhereNotIn(expression, values);
            return this;
        }

        //TODO 
        public IList<T> ToList()
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                return connection.Query<T>(_query.QueryString, _query.QueryParameters, commandType: CommandType.Text).ToList();
            }
        }

        public async Task<IList<T>> ToListAsync()
        {
            using (var connection = await DbConnectionFactory.OpenConnectionAsync())
            {
                var task = await connection.QueryAsync<T>(_query.QueryString, _query.QueryParameters, commandType: CommandType.Text);
                return task.ToList();
            }
        }

        public IList<T> ToListWithTransaction(IDbConnection connection, IDbTransaction transaction = null)
        {
            return connection.Query<T>(_query.QueryString, _query.QueryParameters, transaction: transaction, commandType: CommandType.Text).ToList();
        }

        public async Task<IList<T>> ToListWithTransactionAsync(IDbConnection connection, IDbTransaction transaction = null)
        {
            var task = await connection.QueryAsync<T>(_query.QueryString, _query.QueryParameters, transaction: transaction, commandType: CommandType.Text);
            return task.ToList();
        }

        public async Task<IPageList<T>> ToPageListAsync()
        {
            using (var connection = await DbConnectionFactory.OpenConnectionAsync())
            {
                var reader = await connection.QueryMultipleAsync(_query.QueryString, _query.QueryParameters, commandType: CommandType.Text);
                var list = await reader.ReadAsync<T>();
                var total = (await reader.ReadAsync<int>()).Single();

                return new PageList<T>(list, -1, -1, total);
            }
        }

        public async Task<IPageList<T>> ToPageListWithTransactionAsync(IDbConnection connection, IDbTransaction transaction = null)
        {
            var reader = await connection.QueryMultipleAsync(_query.QueryString, _query.QueryParameters, transaction: transaction, commandType: CommandType.Text);
            var list = await reader.ReadAsync<T>();
            var total = (await reader.ReadAsync<int>()).Single();

            return new PageList<T>(list, -1, -1, total);
        }

        //TODO
        public T SingleOrDefault()
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                return connection.Query<T>(
                    _query.QueryString,
                    _query.QueryParameters, 
                    commandType: CommandType.Text).SingleOrDefault();
            }
        }

        public async Task<T> SingleOrDefaultAsync()
        {
            using (var connection = await DbConnectionFactory.OpenConnectionAsync())
            {
                var task = await connection.QueryAsync<T>(
                        _query.QueryString,
                        _query.QueryParameters,
                        commandType: CommandType.Text);
                return task.SingleOrDefault();
            }
        }

        public T SingleOrDefaultWithTransaction(IDbConnection connection, IDbTransaction transaction = null)
        {
            return connection.Query<T>(
                _query.QueryString, 
                _query.QueryParameters, 
                transaction: transaction, 
                commandType: CommandType.Text).SingleOrDefault();
        }

        public async Task<T> SingleOrDefaultWithTransactionAsync(IDbConnection connection, IDbTransaction transaction = null)
        {
            var task = await connection.QueryAsync<T>(
                        _query.QueryString,
                        _query.QueryParameters,
                        transaction: transaction,
                        commandType: CommandType.Text);
            return task.SingleOrDefault();
        }

        //TODO
        public int Count()
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                return connection.ExecuteScalar<int>(_query.QueryString, _query.QueryParameters, commandType: CommandType.Text);
            }
        }

        public async Task<int> CountAsync()
        {
            using (var connection = await DbConnectionFactory.OpenConnectionAsync())
            {
                return await connection.ExecuteScalarAsync<int>(
                    _query.QueryString,
                    _query.QueryParameters,
                    commandType: CommandType.Text);
            }
        }

        public int CountWithTransaction(IDbConnection connection, IDbTransaction transaction = null)
        {
            return connection.ExecuteScalar<int>(
                _query.QueryString,
                _query.QueryParameters,
                transaction: transaction,
                commandType: CommandType.Text);
        }
        public async Task<int> CountAsyncWithTransaction(IDbConnection connection, IDbTransaction transaction = null)
        {
            return await connection.ExecuteScalarAsync<int>(
                    _query.QueryString,
                    _query.QueryParameters,
                    transaction: transaction,
                    commandType: CommandType.Text);
        }
    }
}

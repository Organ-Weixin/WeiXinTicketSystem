using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Repository
{
    /// <summary>
    /// 创建数据库连接
    /// </summary>
    public class DbConnectionFactory
    {
        public static SqlConnection OpenSqlConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString;
            var conn = new SqlConnection(connectionString);
            conn.Open();

            return conn;
        }

        /// <summary>
        /// 根据配置文件中的默认配置同步创建数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection OpenConnection()
        {
            var connection = ConfigurationManager.ConnectionStrings["ConnectionString2"];
            return OpenConnection(connection.ProviderName, connection.ConnectionString);
        }

        /// <summary>
        /// 根据传入的连接名称和连接字符串同步创建数据库连接
        /// </summary>
        /// <param name="providerInvariantName"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDbConnection OpenConnection(string providerInvariantName, string connectionString)
        {
            var factory = DbProviderFactories.GetFactory(providerInvariantName);
            var conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 根据配置文件中的默认配置异步创建数据库连接
        /// </summary>
        /// <returns></returns>
        public static async Task<IDbConnection> OpenConnectionAsync()
        {
            var connection = ConfigurationManager.ConnectionStrings["ConnectionString2"];
            return await OpenConnectionAsync(connection.ProviderName, connection.ConnectionString);
        }

        /// <summary>
        /// 根据传入的连接名称和连接字符串异步创建数据库连接
        /// </summary>
        /// <param name="providerInvariantName"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static async Task<IDbConnection> OpenConnectionAsync(string providerInvariantName, string connectionString)
        {
            var factory = DbProviderFactories.GetFactory(providerInvariantName);
            var conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;
            await conn.OpenAsync();
            return conn;
        }
    }
}

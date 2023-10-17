using Dapper;
using DataDog.Database.MSSQL;
using DataDog.Database.MYSQL;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace DataDog.Database
{
    public abstract class BaseSQLProvider
    {

        private readonly IDbConnection _context;

        public BaseSQLProvider(IDbConnection context)
        {
            _context = context;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">新增資料結構</typeparam>
        /// <param name="data">資料</param>
        /// <returns></returns>
        public virtual async Task Create<T>(T data)
        {
            if (data is not string sql)
                throw new ArgumentException("新增資料不可為空");

            using var connection = _context;
            connection.Open();
            await connection.ExecuteAsync(sql);
            connection.Close();
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <typeparam name="T">刪除資料結構</typeparam>
        /// <param name="data">刪除資料</param>
        /// <returns></returns>
       
        public virtual async Task Delete<T>(T data)
        {
            if (data is not string sql)
                throw new ArgumentException("刪除資料不可為空");

            using var connection = _context;
            connection.Open();
            await connection.ExecuteAsync(sql);
            connection.Close();
        }

        /// <summary>
        /// 讀取
        /// </summary>
        /// <typeparam name="T">讀取資料結構</typeparam>
        /// <param name="data">讀取條件</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> Read<T>(object data)
        {
            if (data is not string sql)
                throw new ArgumentException("讀取資料不可為空");

            using var connection = _context;
            connection.Open();
            var result = await connection.QueryAsync<T>(sql);
            connection.Close();
            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T">修改資料結構</typeparam>
        /// <param name="data">修改資料</param>
        /// <returns></returns>
        public virtual async Task Update<T>(T data)
        {
            if (data is not string sql)
                throw new ArgumentException("更新資料不可為空");

            using var connection = _context;
            connection.Open();
            await connection.ExecuteAsync(sql);
            connection.Close();
        }
    }

    public static class SQLProviderDI
    {

        /// <summary>
        /// 註冊 MYSQL
        /// </summary>
        /// <param name="services"></param>
        public static void AddMYSQLProvider(this IServiceCollection services, string _connectionString)
        {
            services.AddScoped<IDbConnection>(x => new MySqlConnection(_connectionString));
            services.AddScoped<BaseSQLProvider, MYSQLProvider>();
        }

        /// <summary>
        /// 註冊 MSSQL
        /// </summary>
        /// <param name="services"></param>
        public static void AddMSSQLProvider(this IServiceCollection services, string _connectionString)
        {
            services.AddScoped<IDbConnection>(x => new SqlConnection(_connectionString));
            services.AddScoped<BaseSQLProvider, MSSQLProvider>();
        }
    }

}

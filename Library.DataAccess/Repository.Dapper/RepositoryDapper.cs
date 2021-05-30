using Dapper;
using Library.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Library.DataAccess.Repository.Dapper
{
    public class RepositoryDapper<T> : IRepository<T> where T : class
    {
        protected readonly IDbConnection _connection;
        protected string passedTableName;
        protected readonly string _connectionString;

        public RepositoryDapper(
            IConfiguration configuration,
            IDbConnection connection)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            passedTableName = typeof(T).GetCustomAttribute<TableAttribute>().Name;
            _connection = connection;
        }
        public async void Delete(T entity)
        {
            var sql = $@"DELETE * FROM {passedTableName}
                         WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, entity);

            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    await db.ExecuteAsync(sql, entity);
            //}
        }

        public T Get(int id)
        {
            var sql = $@"SELECT * FROM {passedTableName}
                         WHERE Id = @Id";

            return _connection.Query<T>(sql, new { id }).FirstOrDefault();

            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    return db.Query<T>(sql, new { id }).FirstOrDefault();
            //}

        }

        public IEnumerable<T> GetAll()
        {
            var sql = $@"SELECT * FROM {passedTableName}";

            return _connection.Query<T>(sql);

            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    return db.Query<T>(sql);
            //}
        }

        public async void Insert(T entity)
        {
            var columns = GetColumns();
            string columnsValue = string.Join(", ", columns.Select(e => e));
            string parametrsValue = string.Join(", ", columns.Select(e => "@" + e));
            string sql = $"INSERT INTO {passedTableName} ({columnsValue}) VALUES ({parametrsValue})";

            await _connection.ExecuteAsync(sql, entity);

            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    await db.ExecuteAsync(sql, entity);
            //}
        }

        public async void Update(T entity)
        {
            List<string> columns = GetColumns();
            string columnsValue = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
            string sql = $@"UPDATE {passedTableName} SET {columnsValue}
                         WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, entity);

            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    await db.ExecuteAsync(sql, entity);
            //}
        }
        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
        protected List<string> GetColumns()
        {
            List<string> propertyNames = typeof(T).GetProperties()
                .Where(property => !property.GetGetMethod().IsVirtual &&
                                   !Attribute.IsDefined(property, typeof(ForeignKeyAttribute)) &&
                                   !Attribute.IsDefined(property, typeof(NotMappedAttribute)) &&
                                   !Attribute.IsDefined(property, typeof(DatabaseGeneratedAttribute)) &&
                                   !Attribute.IsDefined(property, typeof(KeyAttribute)))
                .Select(property => property.Name)
                .AsList();
            return propertyNames;
        }
    }
}

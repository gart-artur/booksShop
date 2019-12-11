using Library.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.ComponentModel.DataAnnotations;

namespace Library.DataAccess.Repository.Dapper
{
    public class RepositoryDapper<T> : IRepository<T> where T : class
    {
        protected string passedTableName;

        protected readonly string _connectionString;

        public RepositoryDapper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            passedTableName = typeof(T).GetCustomAttribute<TableAttribute>().Name;
        }
        public async void Delete(T entity)
        {
            var sql = $@"DELETE * FROM {passedTableName}
                         WHERE Id = @Id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(sql, entity);
            }
        }

        public T Get(int id)
        {
            var sql = $@"SELECT * FROM {passedTableName}
                         WHERE Id = @Id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<T>(sql, new { id }).FirstOrDefault();
            }

        }

        public IEnumerable<T> GetAll()
        {
            var sql = $@"SELECT * FROM {passedTableName}";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<T>(sql);
            }
        }

        public async void Insert(T entity)
        {
            var columns = GetColumns();
            string columnsValue = string.Join(", ", columns.Select(e => e));
            string parametrsValue = string.Join(", ", columns.Select(e => "@" + e));
            var sql = $"INSERT INTO {passedTableName} ({columnsValue}) VALUES ({parametrsValue})";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(sql, entity);
            }
        }

        public async void Update(T entity)
        {
            var columns = GetColumns();
            var columnsValue = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
            var sql = $@"UPDATE {passedTableName} SET {columnsValue}
                         WHERE Id = @Id";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(sql, entity);
            }
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

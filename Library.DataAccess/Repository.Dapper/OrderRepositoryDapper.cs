using Dapper;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository.Dapper
{
    public class OrderRepositoryDapper : RepositoryDapper<Order>, IOrderRepository
    {
        public OrderRepositoryDapper(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<IEnumerable<Order>> GetAllOrdersByUserId(string id)
        {
            var sql = $@"SELECT * FROM {passedTableName} WHERE UserId = @Id";

            using(DbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<Order> records = await db.QueryAsync<Order>(sql, new { Id = id });
                return records;
            }
        
        }
    }
}

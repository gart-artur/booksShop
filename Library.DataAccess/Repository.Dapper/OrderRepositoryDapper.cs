using Dapper;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository.Dapper
{
    public class OrderRepositoryDapper : RepositoryDapper<Order>, IOrderRepository
    {
        public OrderRepositoryDapper(IConfiguration configuration, IDbConnection dbConnection) : base(configuration, dbConnection)
        {
        }
        public async Task<IEnumerable<Order>> GetAllOrdersByUserId(string id)
        {
            var sql = $@"SELECT * FROM {passedTableName} WHERE UserId = @Id ORDER BY DateCreated DESC";

            IEnumerable<Order> records = await _connection.QueryAsync<Order>(sql, new { Id = id });
            return records;

            //using (DbConnection db = new SqlConnection(_connectionString))
            //{
            //    IEnumerable<Order> records = await db.QueryAsync<Order>(sql, new { Id = id });
            //    return records;
            //}        
        }
    }
}

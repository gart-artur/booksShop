using Dapper;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace Library.DataAccess.Repository.Dapper
{
    public class BookRepositoryDapper : RepositoryDapper<Book>, IBookRepository
    {
        public BookRepositoryDapper(IConfiguration configuration, IDbConnection dbConnection) : base(configuration, dbConnection)
        {
        }
        public IEnumerable<Book> SortByParams(string name, int minPrice, int maxPrice)
        {
            string encodeNameForLike = "%" + name + "%";
            var sql = $@"SELECT * FROM {passedTableName} WHERE Name LIKE @encodeNameForLike AND Price > {minPrice} AND Price < {maxPrice}";

            IEnumerable<Book> records = _connection.Query<Book>(sql, new { encodeNameForLike });

            return records;

            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    IEnumerable<Book> records = _connection.Query<Book>(sql, new { encodeNameForLike });
            //    return records;
            //}
        }
    }
}
        /* public async Task<IEnumerable<Book>> SortByParams(string name, int minPrice, int maxPrice)
{
    string encodeNameForLike = "%" + name + "%";
    var sql = $@"SELECT * FROM {passedTableName} WHERE Name LIKE @encodeNameForLike AND Price > {minPrice} AND Price < {maxPrice}";

    using (IDbConnection db = new SqlConnection(_connectionString))
    {
        IEnumerable<Book> records = await db.QueryAsync<Book>(sql, new { encodeNameForLike });
        return records;
    }
}*/
    


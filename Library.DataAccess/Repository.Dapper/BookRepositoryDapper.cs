using Dapper;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository.Dapper
{
    public class BookRepositoryDapper : RepositoryDapper<Book>, IBookRepository
    {
        public BookRepositoryDapper(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Book> SortByParams(string name, int minPrice, int maxPrice)
        {
            string encodeNameForLike = "%" + name + "%";
            var sql = $@"SELECT * FROM {passedTableName} WHERE Name LIKE @encodeNameForLike AND Price > {minPrice} AND Price < {maxPrice}";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                IEnumerable<Book> records = db.Query<Book>(sql, new { encodeNameForLike });
                return records;
            }
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
    


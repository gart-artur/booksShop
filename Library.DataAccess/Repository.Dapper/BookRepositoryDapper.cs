using Dapper;
using Library.DataAccess.DTO;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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

        public async Task<List<BookWithAuthorsDto>> GetWithAuthorsByFilter(int skip, int take)
        {
            string sql =
                @$"DECLARE @res TABLE(Id INT, Name NVARCHAR(100), Price INT, AuthorId INT, AuthorName NVARCHAR(100), RowNumber INT)
                INSERT INTO @res
                SELECT
                	b.Id,
                	b.Name,
                	b.Price,
                	a.Id AuthorId,
                	a.Name AuthorName,
                	DENSE_RANK() OVER(ORDER BY b.Name, b.Id) AS RowNumber
                FROM Books b 
                INNER JOIN BooksAndAuthors ba ON ba.BookId = b.Id
                INNER JOIN Authors a ON a.Id = ba.AuthorId
                
                SELECT ISNULL(MAX(RowNumber), 0) FROM @res
                SELECT Id, Name, Price, AuthorId AS Id, AuthorName AS Name FROM @res
                WHERE RowNumber <= {take + skip} AND RowNumber > {skip}";

            var result = new List<BookWithAuthorsDto>();

            using (SqlMapper.GridReader multipleQuery = await _connection.QueryMultipleAsync(sql, commandType: CommandType.Text))
            {
                var items = new Dictionary<int, BookWithAuthorsDto>();

                int total = await multipleQuery.ReadFirstOrDefaultAsync<int>();

                multipleQuery.Read<BookWithAuthorsDto, BookWithAuthorsDtoItems, BookWithAuthorsDto>(
                (book, author) =>
                {
                    if (!items.TryGetValue(book.Id, out BookWithAuthorsDto bookEntry))
                    {
                        bookEntry = book;
                        bookEntry.Total = total;
                        bookEntry.Authors = new List<BookWithAuthorsDtoItems>();

                        items.Add(bookEntry.Id, bookEntry);
                    }

                    if (author != null)
                    {
                        bookEntry.Authors.Add(author);
                    }

                    return bookEntry;
                });

                result = items.Values.AsList();
            };

            return result;
        }
    }
}



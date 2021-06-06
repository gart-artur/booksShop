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
    public class AuthorRepositoryDapper : RepositoryDapper<Author>, IAuthorRepository
    {
        public AuthorRepositoryDapper(IConfiguration configuration, IDbConnection dbConnection) : base(configuration, dbConnection)
        {
        }

        public async Task<List<AuthorWithBooks>> GetWithBooks(int take, int skip)
        {
            string sql =
                @$"DECLARE @res TABLE(Id INT, Name NVARCHAR(100), BookId INT, BookName NVARCHAR(100), Price INT, RowNumber INT)
                INSERT INTO @res
                SELECT
                    a.Id,
                    a.Name,
                    b.Id,
                    b.Name,
                    b.Price,
                    DENSE_RANK() OVER(ORDER BY a.Name, a.Id) AS RowNumber
                FROM Authors a 
                INNER JOIN BooksAndAuthors ba ON ba.AuthorId = a.Id
                INNER JOIN Books b ON b.Id = ba.BookId                
                SELECT ISNULL(MAX(RowNumber), 0) FROM @res
                SELECT Id, Name, BookId AS Id, BookName AS Name, Price FROM @res
                WHERE RowNumber <= {take + skip} AND RowNumber > {skip}";

            var result = new List<AuthorWithBooks>();

            using (SqlMapper.GridReader multipleQuery = await _connection.QueryMultipleAsync(sql, commandType: CommandType.Text))
            {
                var items = new Dictionary<int, AuthorWithBooks>();

                int total = await multipleQuery.ReadFirstOrDefaultAsync<int>();

                multipleQuery.Read<AuthorWithBooks, AuthorWithBooksItem, AuthorWithBooks>(
                (author, book) =>
                {
                    if (!items.TryGetValue(author.Id, out AuthorWithBooks authorEntry))
                    {
                        authorEntry = author;
                        authorEntry.Total = total;
                        authorEntry.Books = new List<AuthorWithBooksItem>();

                        items.Add(authorEntry.Id, authorEntry);
                    }

                    if (author != null)
                    {
                        authorEntry.Books.Add(book);
                    }

                    return authorEntry;
                });

                result = items.Values.AsList();
            };

            return result;
        }
    }
}

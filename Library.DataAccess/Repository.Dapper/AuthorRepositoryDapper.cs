using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Repository.Dapper
{
    public class AuthorRepositoryDapper : RepositoryDapper<Author>, IAuthorRepository
    {
        public AuthorRepositoryDapper(IConfiguration configuration) : base(configuration)
        {
        }

    }
}

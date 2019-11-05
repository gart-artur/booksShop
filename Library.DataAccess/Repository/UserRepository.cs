using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DataAccess.Repository
{
/*
    public class UserRepository : IUserRepository
    {
        protected MagazineContext _context;
        public UserRepository(MagazineContext context)
        {
            _context = context;
        }     
        public void Create(User user)   
        {
            _context.Users.Add(user);
        }
        public User Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Login == login);

            // check if username exists
            if (user == null)
                return null;

            // authentication successful
            return (user);
        }
    }*/
}
using Library.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Interfaces
{
    public interface IUserRepository
    { 
        void Create(User user);
        User Authenticate(string login, string password);
    }
}

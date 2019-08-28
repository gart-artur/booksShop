using Library.BusinessLogic.Services.ViewModel.Account.Post;
using Library.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        User Authenticate(User user);
        //IEnumerable<User> GetAll();
        //void Delete(User user);
        User Create(User user);
    }
}

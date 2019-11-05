using Library.DataAccess.Entities;

namespace Library.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        void Create(User user);
        User Authenticate(string login, string password);
    }
}

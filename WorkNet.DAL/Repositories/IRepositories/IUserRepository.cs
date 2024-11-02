using WorkNet.DAL.Models;

namespace WorkNet.DAL.Repositories.IRepositories
{
    public interface IUserRepository
    {
        //Task<User?> GetUser(string email, string passwordHash);
        Task<User?> GetUser(string email);

        Task<int> AddUser(User user);
    }
}

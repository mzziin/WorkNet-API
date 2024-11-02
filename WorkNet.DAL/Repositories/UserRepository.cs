using Microsoft.EntityFrameworkCore;
using WorkNet.DAL.Data;
using WorkNet.DAL.Models;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WorkNetDbContext _db;
        public UserRepository(WorkNetDbContext workNetDbContext)
        {
            _db = workNetDbContext;
        }
        /*public async Task<User?> GetUser(string email, string passwordHash)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == passwordHash);
                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }*/

        public async Task<User?> GetUser(string email)
        {
            try
            {
                return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<int> AddUser(User user)
        {
            try
            {
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return user.UserId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


    }
}

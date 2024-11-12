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

        public async Task<User?> GetUser(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "Email cannot be null");

            return await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<int> AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user.UserId;
        }
    }
}

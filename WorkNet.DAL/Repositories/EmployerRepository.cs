using WorkNet.DAL.Data;
using WorkNet.DAL.Models;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.DAL.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly WorkNetDbContext _db;
        public EmployerRepository(WorkNetDbContext workNetDbContext)
        {
            _db = workNetDbContext;
        }
        public async Task<bool> AddEmployer(Employer employer)
        {
            try
            {
                await _db.Employers.AddAsync(employer);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // log 
                // throw new Exception(ex.Message, ex);
                return false;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
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
            if (employer == null)
                throw new ArgumentNullException(nameof(employer), "Employer cannot be null");

            await _db.Employers.AddAsync(employer);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Employer?> GetByUserId(int uId)
        {
            if (uId <= 0)
                throw new ArgumentException("UserId cannot be less than or equal to zero", nameof(uId));

            return await _db.Employers.FirstOrDefaultAsync(u => u.UserId == uId);
        }
        public async Task<Employer?> GetByEmployerId(int eId)
        {
            if (eId <= 0)
                throw new ArgumentException("EmployerId cannot be less than or equal to zero", nameof(eId));

            return await _db.Employers.FindAsync(eId);
        }

        public async Task<bool> UpdateEmployer(Employer employer)
        {
            if (employer == null)
                throw new ArgumentNullException("Employer cannot be null", nameof(employer));

            var employerFromDb = await _db.Employers.FindAsync(employer.EmployerId);
            if (employerFromDb == null)
                return false;

            employerFromDb.CompanyName = string.IsNullOrWhiteSpace(employer.CompanyName) ? employerFromDb.CompanyName : employer.CompanyName;
            employerFromDb.Address = string.IsNullOrWhiteSpace(employer.Address) ? employerFromDb.Address : employer.Address;
            employerFromDb.ContactPerson = string.IsNullOrWhiteSpace(employer.ContactPerson) ? employerFromDb.ContactPerson : employer.ContactPerson;
            employerFromDb.Industry = string.IsNullOrWhiteSpace(employer.Industry) ? employerFromDb.Industry : employer.Industry;

            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteEmployer(Employer employer)
        {
            ArgumentNullException.ThrowIfNull(nameof(employer));

            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.Employers.Remove(employer);

                    var user = await _db.Users.FindAsync(employer.UserId);
                    if (user != null)
                        _db.Users.Remove(user);

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception)
                {
                    //log
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}

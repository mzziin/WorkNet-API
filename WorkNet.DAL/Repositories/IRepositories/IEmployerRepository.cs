using WorkNet.DAL.Models;

namespace WorkNet.DAL.Repositories.IRepositories
{
    public interface IEmployerRepository
    {
        Task<bool> AddEmployer(Employer employer);
        Task<Employer?> GetByUserId(int uId);
        Task<Employer?> GetByEmployerId(int eId);
        Task<bool> UpdateEmployer(Employer employer);
        Task<bool> DeleteEmployer(Employer employer);
    }
}
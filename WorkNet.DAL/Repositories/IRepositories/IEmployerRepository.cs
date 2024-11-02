using WorkNet.DAL.Models;

namespace WorkNet.DAL.Repositories.IRepositories
{
    public interface IEmployerRepository
    {
        Task<bool> AddEmployer(Employer employer);
    }
}

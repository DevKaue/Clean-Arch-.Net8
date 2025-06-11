using Infra.Repository.IRepositories;

namespace Infra.Repository.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        void Commit();
    }
}

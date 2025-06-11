using System.Linq.Expressions;

namespace Infra.Repository.UnitOfWork
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        // Get, Update, Delete, GetAll, Create
        public Task<T> Create(T command)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public T? Get(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T commandUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

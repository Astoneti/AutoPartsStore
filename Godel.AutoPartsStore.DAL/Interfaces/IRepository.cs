using Godel.AutoPartsStore.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Godel.AutoPartsStore.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> Get(int id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(int id);
    }
}

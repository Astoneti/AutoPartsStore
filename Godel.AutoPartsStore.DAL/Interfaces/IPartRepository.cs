using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL.Models;
using System.Threading.Tasks;

namespace Godel.AutoPartsStore.DAL.Interfaces
{
    public interface IPartRepository : IRepository<Part>
    {
        Task<PaginatedList<Part>> GetFilteredList(int? categoryId,string search, string sortOrder, int pageSize, int pageNumber);
    }
}

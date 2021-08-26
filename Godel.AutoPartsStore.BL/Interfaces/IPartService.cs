using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Godel.AutoPartsStore.BusinessLogicLayer.Interfaces
{
    public interface IPartService 
    {
        Task<Part> Get(int id);
        Task Create(Part part);
        Task Delete(int id);
        Task Update(Part part);
        Task<PaginatedList<Part>> GetFilteredList(int? categoryId,string search, string sortOrder, int pageSize, int pageNumber);
    }
}


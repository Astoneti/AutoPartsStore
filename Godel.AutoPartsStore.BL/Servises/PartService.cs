using Godel.AutoPartsStore.BusinessLogicLayer.Interfaces;
using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL.Interfaces;
using Godel.AutoPartsStore.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Servises
{
    public class PartService : IPartService
    {
        private IUnitOfWork _unitOfWork;

        public PartService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task Create(Part part)
        {
            await _unitOfWork.Parts.Create(part);
            _unitOfWork.Save();
        }

        public async Task Delete(int id)
        {
           await _unitOfWork.Parts.Delete(id);
           _unitOfWork.Save();
        }

        public async Task<Part> Get(int id)
        {
            return await _unitOfWork.Parts.Get(id);
        }

        public IEnumerable<Part> GetAll()
        {
            return _unitOfWork.Parts.GetAll();
        }

        public async Task<PaginatedList<Part>> GetFilteredList(int? categoryId, string search, string sortOrder, int pageNumber, int pageSize)
        {
            return await _unitOfWork.Parts.GetFilteredList(categoryId, search, sortOrder, pageSize, pageNumber);
        }

        public async Task Update(Part part)
        {
            await  _unitOfWork.Parts.Update(part);
            _unitOfWork.Save();
        }
    }
}

using Godel.AutoPartsStore.BusinessLogicLayer.Interfaces;
using Godel.AutoPartsStore.DAL.Interfaces;
using Godel.AutoPartsStore.DAL.Models;
using System.Collections.Generic;

namespace Godel.AutoPartsStore.BusinessLogicLayer.Servises
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<Category> GetAll()
        {
            return _unitOfWork.Categories.GetAll();
        }
    }


}

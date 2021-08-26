using Godel.AutoPartsStore.DAL.Models;
using System.Collections.Generic;

namespace Godel.AutoPartsStore.BusinessLogicLayer.Interfaces
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetAll();
    }
}

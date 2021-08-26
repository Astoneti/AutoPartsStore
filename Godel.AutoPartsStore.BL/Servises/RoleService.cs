using Godel.AutoPartsStore.BL.Interfaces;
using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godel.AutoPartsStore.BL.Servises
{
    public class RoleService : IRoleService
    {
        private IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public Role GetRoleByName(string role)
        {
           return _unitOfWork.Roles.GetRoleByName(role);
        }
    }
}

using Godel.AutoPartsStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godel.AutoPartsStore.Tests
{
    internal class PartComparer : IEqualityComparer<Part>
    {
        public bool Equals(Part x, Part y)
        {
            return x.Id.Equals(y.Id) && x.Name.Equals(y.Name);
        }

        public int GetHashCode(Part obj)
        {
            return obj.GetHashCode();
        }
    }
}

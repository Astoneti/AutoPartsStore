using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Godel.AutoPartsStore.DAL.Interfaces;
using Godel.AutoPartsStore.DAL.Models;
using Godel.AutoPartsStore.Common.Models;

namespace Godel.AutoPartsStore.DAL.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly PartsStoreDbContext db;

        public PartRepository(PartsStoreDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Part> GetAll()
        {
            return db.Parts;
        }

        public async Task Create(Part part)
        {
            await db.Parts.AddAsync(part);
            db.SaveChanges();
        }

        public async Task Update(Part part)
        {
            db.Entry(part).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Part part = db.Parts.Find(id);
            if (part != null) 
            {
                db.Parts.Remove(part);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Part> Get(int id)
        {
           return await db.Parts.FirstOrDefaultAsync(p => p.Id == id);
           
        }        

        public async Task<PaginatedList<Part>> GetFilteredList(int? categoryId, string search, string sortOrder, int pageSize, int pageNumber)
        {
            var parts = db.Parts.Include(p => p.Category).Where(p => string.IsNullOrEmpty(search) || p.Name.Contains(search)).Where(p => !categoryId.HasValue || p.CategoryId==categoryId.Value);
           
            switch (sortOrder)
            {
                case "Category":
                    parts = parts.OrderBy(p => p.Category);
                    break;
                case "category_desc":
                    parts = parts.OrderByDescending(p => p.Category);
                    break;
                case "Name":
                    parts = parts.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    parts = parts.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    parts = parts.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    parts = parts.OrderByDescending(p => p.Price);
                    break;
                default:
                    parts = parts.OrderBy(p => p.Price);
                    break;
            }
            return await PaginatedList<Part>.CreateAsync(parts, pageNumber, pageSize);
        }
    }
}

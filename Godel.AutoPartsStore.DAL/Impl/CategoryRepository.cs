using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Godel.AutoPartsStore.DAL.Interfaces;
using Godel.AutoPartsStore.DAL.Models;
using System.Threading.Tasks;

namespace Godel.AutoPartsStore.DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly PartsStoreDbContext context;

        public CategoryRepository(PartsStoreDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public async Task Create(Category category)
        {
            await context.Categories.AddAsync(category);
            context.SaveChanges();
        }

        public async Task Update(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public IEnumerable<Category> Find(Func<Category, Boolean> predicate)
        {
            return context.Categories.Include(c => c.Parts).Where(predicate).ToList();
        }

        public async Task Delete(int id)
        {
            Category category = context.Categories.Find(id);
            if (category != null)
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Category> Get(int id)
        {
            return await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }        
    }
}

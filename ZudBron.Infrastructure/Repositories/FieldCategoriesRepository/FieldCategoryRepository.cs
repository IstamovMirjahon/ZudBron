using Microsoft.EntityFrameworkCore;
using System;
using ZudBron.Domain.Models.FieldCategories;

namespace ZudBron.Infrastructure.Repositories.FieldCategoriesRepository
{
    public class FieldCategoryRepository : IFieldCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public FieldCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FieldCategory>> GetAllAsync()
        {
            return await _context.FieldCategories.ToListAsync();
        }

        public async Task<FieldCategory?> GetByIdAsync(Guid id)
        {
            return await _context.FieldCategories.FindAsync(id);
        }

        public async Task AddAsync(FieldCategory category)
        {
            await _context.FieldCategories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FieldCategory category)
        {
            _context.FieldCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(FieldCategory category)
        {
            _context.FieldCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

}

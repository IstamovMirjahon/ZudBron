
using Microsoft.EntityFrameworkCore;
using ZudBron.Application.IService.IFieldServices;
using ZudBron.Domain.DTOs.FieldDTO;
using ZudBron.Domain.Models.FieldCategories;
using ZudBron.Domain.Models.Media;
using ZudBron.Domain.Models.SportFieldModels;

namespace ZudBron.Infrastructure.Services.FieldService
{
    public class SportFieldService : ISportFieldService
    {
        private readonly ApplicationDbContext _context;

        public SportFieldService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SportFieldDto>> GetAllAsync()
        {
            return await _context.SportFields
                .Include(x => x.Location)
                .Include(x => x.MediaFiles)
                .Include(x => x.Owner)
                .Include(x=>x.Category)
                .Select(x => new SportFieldDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    PricePerHour = x.PricePerHour,
                    Description = x.Description,
                    OpenHour = x.OpenHour,
                    CloseHour = x.CloseHour,
                    LocationId = x.LocationId,
                    LocationName = x.Location!.AddressLine,
                    CategoryId = x.Category.Id, // yangi qo‘shildi
                    CategoryName = x.Category!.Name, // yangi qo‘shildi
                    OwnerId = x.OwnerId,
                    OwnerFullName = x.Owner!.FullName,
                    MediaUrls = x.MediaFiles!.Select(m => m.FilePath).ToList()
                }).ToListAsync();
        }

        public async Task<SportFieldDto?> GetByIdAsync(Guid id)
        {
            var field = await _context.SportFields
                .Include(x => x.Location)
                .Include(x => x.MediaFiles)
                .Include(x => x.Owner)
                .Include (x => x.Category)  
                .FirstOrDefaultAsync(x => x.Id == id);

            if (field == null) return null;

            return new SportFieldDto
            {
                Id = field.Id,
                Name = field.Name,
                PricePerHour = field.PricePerHour,
                Description = field.Description,
                OpenHour = field.OpenHour,
                CloseHour = field.CloseHour,
                LocationId = field.LocationId,
                LocationName = field.Location?.AddressLine,
                CategoryId = field.Category.Id, // yangi qo‘shildi
                CategoryName = field.Category!.Name, // yangi qo‘shildi
                OwnerId = field.OwnerId,
                OwnerFullName = field.Owner?.FullName,
                MediaUrls = field.MediaFiles?.Select(m => m.FilePath).ToList()
            };
        }

        public async Task<Guid> CreateAsync(CreateOrUpdateSportFieldDto dto)
        {
            var sportField = new SportField
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                PricePerHour = dto.PricePerHour,
                Description = dto.Description,
                OpenHour = dto.OpenHour,
                CloseHour = dto.CloseHour,
                LocationId = dto.LocationId,
                CategoryId = dto.CategoryId,
                OwnerId = dto.OwnerId,
                MediaFiles = dto.MediaFileIds != null
                    ? await _context.MediaFiles.Where(m => dto.MediaFileIds.Contains(m.Id)).ToListAsync()
                    : new List<MediaFile>()
            };

            _context.SportFields.Add(sportField);
            await _context.SaveChangesAsync();
            return sportField.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, CreateOrUpdateSportFieldDto dto)
        {
            var field = await _context.SportFields
                .Include(x => x.MediaFiles)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (field == null) return false;

            field.Name = dto.Name;
            field.PricePerHour = dto.PricePerHour;
            field.Description = dto.Description;
            field.OpenHour = dto.OpenHour;
            field.CloseHour = dto.CloseHour;
            field.LocationId = dto.LocationId;
            field.CategoryId = dto.CategoryId;
            field.OwnerId = dto.OwnerId;

            if (dto.MediaFileIds != null)
            {
                field.MediaFiles = await _context.MediaFiles
                    .Where(m => dto.MediaFileIds.Contains(m.Id)).ToListAsync();
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var field = await _context.SportFields.FindAsync(id);
            if (field == null) return false;

            _context.SportFields.Remove(field);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReviewDto>> GetReviewsBySportFieldIdAsync(Guid sportFieldId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.SportFieldId == sportFieldId)
                .Include(r => r.User)
                .Include(r => r.MediaFile)
                .OrderByDescending(r => r.Created)
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Content = r.Content,
                    Rating = r.Rating,
                    CreatedAt = r.Created,
                    AuthorFullName = r.User.FullName,
                    MediaUrl = r.MediaFile != null ? r.MediaFile.FilePath : null
                })
                .ToListAsync();

            return reviews;
        }

        public async Task<List<SportFieldDto>> FilterAsync(SportFieldFilterDto filter)
        {
            var query = _context.SportFields
                .Include(f => f.Location)
                .Include(f => f.Owner)
                .Include(f => f.MediaFiles)
                .Include(f => f.Reviews)
                .Include(f=>f.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(f => f.Name!.ToLower().Contains(filter.Name.ToLower()));

            if (filter.LocationId.HasValue)
                query = query.Where(f => f.LocationId == filter.LocationId.Value);

            if (filter.CategoryId.HasValue)
                query = query.Where(f => f.CategoryId == filter.CategoryId.Value);


            if (filter.MinPrice.HasValue)
                query = query.Where(f => f.PricePerHour >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(f => f.PricePerHour <= filter.MaxPrice.Value);

            if (filter.DesiredStartTime.HasValue)
                query = query.Where(f => f.OpenHour <= filter.DesiredStartTime.Value);

            if (filter.DesiredEndTime.HasValue)
                query = query.Where(f => f.CloseHour >= filter.DesiredEndTime.Value);

            var fields = await query.ToListAsync();

            var result = fields.Select(f => new SportFieldDto
            {
                Id = f.Id,
                Name = f.Name,
                PricePerHour = f.PricePerHour,
                Description = f.Description,
                OpenHour = f.OpenHour,
                CloseHour = f.CloseHour,
                LocationId = f.LocationId,
                LocationName = f.Location?.AddressLine,
                CategoryId = f.CategoryId,
                CategoryName = f.Category?.Name,
                OwnerId = f.OwnerId,
                OwnerFullName = f.Owner?.FullName,
                MediaUrls = f.MediaFiles?.Select(m => m.FilePath).ToList(),
                AverageRating = f.Reviews != null && f.Reviews.Any()
                    ? Math.Round(f.Reviews.Average(r => r.Rating), 1)
                    : null
            }).ToList();

            return result;
        }


    }

}



using Article.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using ZudBron.Application.IService.ILocationServices;
using ZudBron.Domain.Abstractions;
using ZudBron.Domain.DTOs.LocationDTO;
using ZudBron.Domain.Models.SportFieldModels;

namespace ZudBron.Infrastructure.Services.LocationServices
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitofwork;
        public LocationService(ApplicationDbContext context,IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitofwork = unitOfWork;
        }

        public async Task<Result<Guid>> CreateLocationAsync(CreateLocationDto dto)
        {
            try
            {
                var location = new Location
                {
                    Id = Guid.NewGuid(),
                    AddressLine = dto.AddressLine,
                    City = dto.City,
                    Region = dto.Region,
                    Country = dto.Country,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude
                };

                _context.Locations.Add(location);
                await _unitofwork.SaveChangesAsync();
                return Result<Guid>.Success(location.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(new Error("LocationService.CreateLocationAsync()", ex.Message));
            }
        }

        public async Task<Result<RouteResponseDto>> GetGoogleMapsRouteAsync(RouteRequestDto request)
        {
            try
            {
                var sportField = await _context.SportFields
                    .Include(f => f.Location)
                    .FirstOrDefaultAsync(f => f.Id == request.SportFieldId);

                if (sportField == null || sportField.Location == null)
                    throw new Exception("Sport field yoki uning manzili topilmadi");

                var origin = $"{request.UserLatitude.ToString(CultureInfo.InvariantCulture)},{request.UserLongitude.ToString(CultureInfo.InvariantCulture)}";
                var destination = $"{sportField.Location.Latitude.ToString(CultureInfo.InvariantCulture)},{sportField.Location.Longitude.ToString(CultureInfo.InvariantCulture)}";

                var url = $"https://www.google.com/maps/dir/?api=1&origin={origin}&destination={destination}&travelmode=driving";
                var top= new RouteResponseDto { GoogleMapsUrl = url };

                return Result<RouteResponseDto>.Success(top);
            }
            catch (Exception ex)
            {
                return Result<RouteResponseDto>.Failure(new Error("LocationService.GetGoogleMapsRouteAsync()", ex.Message));
            }
        }
    }
}

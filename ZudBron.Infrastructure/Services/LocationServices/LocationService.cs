

using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using ZudBron.Application.IService.ILocationServices;
using ZudBron.Domain.DTOs.LocationDTO;
using ZudBron.Domain.Models.SportFieldModels;

namespace ZudBron.Infrastructure.Services.LocationServices
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;

        public LocationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateLocationAsync(CreateLocationDto dto)
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
            await _context.SaveChangesAsync();
            return location.Id;
        }

        public async Task<RouteResponseDto> GetGoogleMapsRouteAsync(RouteRequestDto request)
        {
            var sportField = await _context.SportFields
                .Include(f => f.Location)
                .FirstOrDefaultAsync(f => f.Id == request.SportFieldId);

            if (sportField == null || sportField.Location == null)
                throw new Exception("Sport field yoki uning manzili topilmadi");

            var origin = $"{request.UserLatitude.ToString(CultureInfo.InvariantCulture)},{request.UserLongitude.ToString(CultureInfo.InvariantCulture)}";
            var destination = $"{sportField.Location.Latitude.ToString(CultureInfo.InvariantCulture)},{sportField.Location.Longitude.ToString(CultureInfo.InvariantCulture)}";

            var url = $"https://www.google.com/maps/dir/?api=1&origin={origin}&destination={destination}&travelmode=driving";

            return new RouteResponseDto { GoogleMapsUrl = url };
        }
    }
}



using ZudBron.Domain.DTOs.LocationDTO;

namespace ZudBron.Application.IService.ILocationServices
{
    public interface ILocationService
    {
        Task<RouteResponseDto> GetGoogleMapsRouteAsync(RouteRequestDto request);
        Task<Guid> CreateLocationAsync(CreateLocationDto dto);
    }
}

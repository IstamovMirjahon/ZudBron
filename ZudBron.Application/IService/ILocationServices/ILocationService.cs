

using Article.Domain.Abstractions;
using ZudBron.Domain.DTOs.LocationDTO;

namespace ZudBron.Application.IService.ILocationServices
{
    public interface ILocationService
    {
        Task<Result<RouteResponseDto>> GetGoogleMapsRouteAsync(RouteRequestDto request);
        Task<Result<Guid>> CreateLocationAsync(CreateLocationDto dto);
    }
}



using ZudBron.Domain.DTOs.NotificationDTOs;

namespace ZudBron.Application.IService.INotificationServices
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetUserNotificationsAsync(Guid userId);
        Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto dto);
        Task MarkAsReadAsync(Guid notificationId);
    }

}

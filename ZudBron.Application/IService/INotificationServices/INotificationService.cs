

using Article.Domain.Abstractions;
using ZudBron.Domain.DTOs.NotificationDTOs;

namespace ZudBron.Application.IService.INotificationServices
{
    public interface INotificationService
    {
        Task<Result<List<NotificationDto>>> GetUserNotificationsAsync(Guid userId);
        Task<Result<NotificationDto>> CreateNotificationAsync(CreateNotificationDto dto);
        Task MarkAsReadAsync(Guid notificationId);
        Task MarkAllAsReadAsync(Guid userId);

    }

}

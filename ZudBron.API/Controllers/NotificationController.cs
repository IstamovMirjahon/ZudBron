using Microsoft.AspNetCore.Mvc;
using ZudBron.Application.IService.INotificationServices;
using ZudBron.Domain.DTOs.NotificationDTOs;

namespace ZudBron.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNotificationDto dto)
        {
            await _notificationService.CreateNotificationAsync(dto);
            return Ok(new { message = "Notification sent successfully." });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserNotifications(Guid userId)
        {
            var notifications = await _notificationService.GetUserNotificationsAsync(userId);
            return Ok(notifications);
        }

        [HttpPost("mark-as-read/{id}")]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            await _notificationService.MarkAsReadAsync(id);
            return Ok(new { message = "Notification marked as read." });
        }
    }
}

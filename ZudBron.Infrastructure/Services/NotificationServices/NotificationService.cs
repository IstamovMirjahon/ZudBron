
using Article.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using ZudBron.Application.IService.INotificationServices;
using ZudBron.Domain.Abstractions;
using ZudBron.Domain.DTOs.NotificationDTOs;
using ZudBron.Domain.Enums.NotificationEnum;
using ZudBron.Domain.Models.NotificationModels;

namespace ZudBron.Infrastructure.Services.NotificationServices
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitofwork;
        public NotificationService(ApplicationDbContext context, IUnitOfWork unitofwork)
        {
            _context = context;
            _unitofwork= unitofwork;
        }

        public async Task<Result<List<NotificationDto>>> GetUserNotificationsAsync(Guid userId)
        {
            try
            {
                var top = await _context.Notifications
                    .Where(n => n.UserId == userId)
                    .OrderByDescending(n => n.SentAt)
                    .Select(n => new NotificationDto
                    {
                        Id = n.Id,
                        Message = n.Message,
                        Type = n.Type.ToString(),
                        IsRead = n.IsRead,
                        SentAt = n.SentAt
                    })
                    .ToListAsync();
                return Result<List<NotificationDto>>.Success(top);
            }
            catch(Exception ex)
            {
                return Result<List<NotificationDto>>.Failure(new Error("NotificationService.GetUserNotificationsAsync()", ex.Message));
            }
        }

        public async Task<Result<NotificationDto>> CreateNotificationAsync(CreateNotificationDto dto)
        {
            try
            {
                var notification = new Notification
                {
                    UserId = dto.UserId,
                    Message = dto.Message,
                    Type = Enum.Parse<NotificationType>(dto.Type),
                    SentAt = DateTime.UtcNow,
                    IsRead = false
                };

                _context.Notifications.Add(notification);
                await _unitofwork.SaveChangesAsync();
                var top = new NotificationDto
                {
                    Id = notification.Id,
                    Message = notification.Message,
                    Type = notification.Type.ToString(),
                    IsRead = notification.IsRead,
                    SentAt = notification.SentAt
                };
                return Result<NotificationDto>.Success(top);
            }
            catch(Exception ex)
            {
                return Result<NotificationDto>.Failure(new Error("NotificationService.CreateNotificationAsync()", ex.Message));
            }
        }

        public async Task MarkAsReadAsync(Guid notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null) return;

            notification.IsRead = true;
            await _unitofwork.SaveChangesAsync();
        }

        public async Task MarkAllAsReadAsync(Guid userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _unitofwork.SaveChangesAsync();
        }

    }

}

﻿using System.ComponentModel.DataAnnotations;
using ZudBron.Domain.Abstractions;

namespace ZudBron.Domain.Models.UserModel
{
    public class ChangeUserEmailOrPhoneNumber : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; } = null!;

        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; } = null!;

        public int VerificationCode { get; set; }

        public DateTime ExpirationTime { get; set; } = DateTime.UtcNow.AddMinutes(2);
    }
}

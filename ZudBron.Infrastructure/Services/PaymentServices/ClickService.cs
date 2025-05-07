

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using ZudBron.Domain.DTOs.PaymentDTOs;
using ZudBron.Domain.Enums.BookingEnum;
using ZudBron.Domain.Models.PaymentModels;
using ZudBron.Domain.StaticModels.ClickModel;

namespace ZudBron.Infrastructure.Services.PaymentServices
{
    public class ClickService
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _db;

        public ClickService(IConfiguration config, ApplicationDbContext db)
        {
            _config = config;
            _db = db;
        }

        public async Task<IActionResult> PrepareAsync(ClickPrepareDto dto)
        {
            var settings = _config.GetSection("ClickSettings").Get<ClickSettings>();

            var expectedSign = ClickHelper.GenerateSign(
                dto.Click_trans_id.ToString(),
                dto.Service_id.ToString(),
                settings.SecretKey,
                dto.Merchant_trans_id,
                dto.Action.ToString(),
                dto.Sign_time
            );

            if (dto.Sign_string != expectedSign)
            {
                return new JsonResult(new { error = -1, error_note = "Invalid sign string" });
            }

            var payment = new PaymentClick
            {
                TransactionId = dto.Click_trans_id.ToString(),
                MerchantTransactionId = dto.Merchant_trans_id,
                Amount = dto.Amount,
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _db.PaymentClicks.Add(payment);
            await _db.SaveChangesAsync();

            return new JsonResult(new
            {
                error = 0,
                error_note = PaymentStatus.Paid,
                click_trans_id = dto.Click_trans_id,
                merchant_trans_id = dto.Merchant_trans_id,
                merchant_prepare_id = payment.Id
            });
        }

        public async Task<IActionResult> CompleteAsync(ClickPrepareDto dto)
        {
            var payment = await _db.PaymentClicks.FindAsync(int.Parse(dto.Merchant_prepare_id));
            if (payment == null)
            {
                return new JsonResult(new { error = -2, error_note = "Payment not found" });
            }

            payment.Status = dto.Error == 0 ? PaymentStatus.Paid : PaymentStatus.Failed;
            await _db.SaveChangesAsync();

            return new JsonResult(new
            {
                error = 0,
                error_note = PaymentStatus.Paid,
                click_trans_id = dto.Click_trans_id,
                merchant_trans_id = dto.Merchant_trans_id,
                merchant_confirm_id = payment.Id
            });
        }
    }

}

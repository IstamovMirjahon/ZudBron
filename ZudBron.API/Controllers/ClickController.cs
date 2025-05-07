using Microsoft.AspNetCore.Mvc;
using ZudBron.Domain.DTOs.PaymentDTOs;
using ZudBron.Domain.Models.PaymentModels;
using ZudBron.Infrastructure.Services.PaymentServices;

namespace ZudBron.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClickController : ControllerBase
    {
        private readonly ClickService _clickService;

        public ClickController(ClickService clickService)
        {
            _clickService = clickService;
        }

        [HttpPost("prepare")]
        public async Task<IActionResult> Prepare([FromForm] ClickPrepareDto dto)
        {
            return await _clickService.PrepareAsync(dto);
        }

        [HttpPost("complete")]
        public async Task<IActionResult> Complete([FromForm] ClickPrepareDto dto)
        {
            return await _clickService.CompleteAsync(dto);
        }

        [HttpGet("create-payment")]
        public IActionResult CreatePayment(string orderId, int amount)
        {
            var settings = new ClickSettings(); // or inject via IOptions
            var url = $"{settings.BaseUrl}?service_id={settings.ServiceId}" +
                      $"&merchant_id={settings.MerchantId}" +
                      $"&amount={amount}" +
                      $"&transaction_param={orderId}" +
                      $"&return_url={settings.ReturnUrl}";

            return Redirect(url);
        }
    }

}

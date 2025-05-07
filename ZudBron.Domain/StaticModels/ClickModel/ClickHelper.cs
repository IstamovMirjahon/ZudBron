
using System.Security.Cryptography;
using System.Text;

namespace ZudBron.Domain.StaticModels.ClickModel
{
    public static class ClickHelper
    {
        public static string GenerateSign(string clickTransId, string serviceId, string secretKey,
                                          string merchantTransId, string action, string signTime)
        {
            var input = $"{clickTransId}{serviceId}{secretKey}{merchantTransId}{action}{signTime}";
            using var md5 = MD5.Create();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}

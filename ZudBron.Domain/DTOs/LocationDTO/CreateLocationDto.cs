
namespace ZudBron.Domain.DTOs.LocationDTO
{
    public class CreateLocationDto
    {
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}

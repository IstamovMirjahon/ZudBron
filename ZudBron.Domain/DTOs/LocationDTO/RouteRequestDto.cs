namespace ZudBron.Domain.DTOs.LocationDTO
{
    public class RouteRequestDto
    {
        public double UserLatitude { get; set; }
        public double UserLongitude { get; set; }

        public Guid SportFieldId { get; set; } // maydon ID
    }
}

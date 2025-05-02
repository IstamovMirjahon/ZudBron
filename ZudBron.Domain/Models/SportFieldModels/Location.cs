using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.Models.SportFieldModels
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; } // EF uchun primary key

        [Required(ErrorMessage = "AddressLine majburiy")]
        [MaxLength(200, ErrorMessage = "AddressLine 200 belgidan oshmasligi kerak")]
        public string? AddressLine { get; set; }

        [Required(ErrorMessage = "Shahar nomi majburiy")]
        [MaxLength(100, ErrorMessage = "City 100 belgidan oshmasligi kerak")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Viloyat nomi majburiy")]
        [MaxLength(100, ErrorMessage = "Region 100 belgidan oshmasligi kerak")]
        public string? Region { get; set; }

        [Required(ErrorMessage = "Davlat nomi majburiy")]
        [MaxLength(100, ErrorMessage = "Country 100 belgidan oshmasligi kerak")]
        public string? Country { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude -90 va 90 oralig‘ida bo‘lishi kerak")]
        public double Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude -180 va 180 oralig‘ida bo‘lishi kerak")]
        public double Longitude { get; set; }
    }
}

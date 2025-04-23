
namespace ZudBron.Domain.Models.SportFieldModels
{
    public class Location
    {
        public Guid Id { get; set; } // EF uchun primary key
        public string? AddressLine { get; set; }      // Masalan: "Chilonzor 9-kvartal, 12-uy"
        public string? City { get; set; }             // Masalan: "Toshkent"
        public string? Region { get; set; }           // Masalan: "Toshkent viloyati"
        public string? Country { get; set; }          // Masalan: "O‘zbekiston"
        public double Latitude { get; set; }         // Masalan: 41.2995
        public double Longitude { get; set; }        // Masalan: 69.2401
    }


}

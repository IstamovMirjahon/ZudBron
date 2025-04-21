namespace ZudBron.Domain.Models.Media
{
    public class MediaFileTag
    {
        public Guid MediaFileId { get; set; }
        public MediaFile MediaFile { get; set; } = default!;

        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = default!;
    }

}

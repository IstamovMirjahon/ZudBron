using ZudBron.Domain.Abstractions;

namespace ZudBron.Domain.Models.Media
{
    public class MediaFile : BaseEntity
    {
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSize { get; set; }

        public ICollection<MediaFileTag> MediaFileTags { get; set; } = new List<MediaFileTag>();
    }

}

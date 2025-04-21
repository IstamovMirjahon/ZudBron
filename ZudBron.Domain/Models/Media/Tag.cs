namespace ZudBron.Domain.Models.Media
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<MediaFileTag> MediaFileTags { get; set; } = new List<MediaFileTag>();
    }

}

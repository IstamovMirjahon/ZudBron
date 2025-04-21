namespace ZudBron.Domain.Abstractions
{
    public abstract class BaseParams : BaseEntity
    {
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

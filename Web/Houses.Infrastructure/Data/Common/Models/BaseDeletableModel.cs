namespace Houses.Infrastructure.Data.Common.Models
{
    public abstract class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletableEntity
    {
        public bool IsActive { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

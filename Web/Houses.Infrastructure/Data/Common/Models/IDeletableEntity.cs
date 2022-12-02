namespace Houses.Infrastructure.Data.Common.Models
{
    public interface IDeletableEntity
    {
        bool IsActive { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}

using EOP.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EOP.Infrastructure.Context.Configuration
{
    public class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Common configurations for CreatedBy, CreatedDate, UpdatedBy, UpdatedDate.
            builder.Property(e => e.CreatedBy).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            // For UpdatedBy and UpdatedDate, no need to add .IsRequired() as these can be null.
        }
    }
}

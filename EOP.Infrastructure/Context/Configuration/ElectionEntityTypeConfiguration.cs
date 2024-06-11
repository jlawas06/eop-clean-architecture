using EOP.Domain.Entities;
using EOP.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EOP.Infrastructure.Context.Configuration
{
    public class ElectionEntityTypeConfiguration : BaseEntityTypeConfiguration<Election>
    {
        public override void Configure(EntityTypeBuilder<Election> builder)
        {
            base.Configure(builder);
            builder.ToTable("Elections");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(150);
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.StartDate).IsRequired();
            builder.Property(e => e.EndDate).IsRequired();
            builder.Property(e => e.Status).IsRequired()
                .HasConversion(
                    e => e.ToString(),
                    e => (ElectionStatusEnum)Enum.Parse(typeof(ElectionStatusEnum), e)
                )
                .HasDefaultValue(ElectionStatusEnum.NotStarted);
            builder.HasMany(e => e.Candidates).WithOne(c => c.Election).HasForeignKey(c => c.ElectionId);
        }
    }
}

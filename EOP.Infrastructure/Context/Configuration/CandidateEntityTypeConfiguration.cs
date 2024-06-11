using EOP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EOP.Infrastructure.Context.Configuration
{
    public class CandidateEntityTypeConfiguration : BaseEntityTypeConfiguration<Candidate>
    {
        public override void Configure(EntityTypeBuilder<Candidate> builder)
        {
            base.Configure(builder);
            builder.ToTable("Candidates");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
            builder.Property(c => c.ElectionId).IsRequired();
            builder.HasOne(c => c.Election).WithMany(e => e.Candidates).HasForeignKey(c => c.ElectionId);
        }
    }
}

using EOP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EOP.Infrastructure.Context.Configuration
{
    public class VoteEntityTypeConfiguration : BaseEntityTypeConfiguration<Vote>
    {
        public override void Configure(EntityTypeBuilder<Vote> builder)
        {
            base.Configure(builder);
            builder.ToTable("Votes");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.CandidateId).IsRequired();
            builder.HasOne(c => c.Candidate).WithMany(e => e.Votes).HasForeignKey(c => c.CandidateId);
            builder.HasOne(c => c.User).WithMany(e => e.Votes).HasForeignKey(c => c.UserId);
        }
    }
}

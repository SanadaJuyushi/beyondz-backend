using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class StackOverflowTagConfiguration : IEntityTypeConfiguration<StackOverflowTag>
    {
        public void Configure(EntityTypeBuilder<StackOverflowTag> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(255);
        }
    }
}

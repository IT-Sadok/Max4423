using BookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.Data.Configurations;

public class ImportProgressConfiguration: IEntityTypeConfiguration<ImportProgress>
{
    public void Configure(EntityTypeBuilder<ImportProgress> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.FileName).IsUnique();
    }
}
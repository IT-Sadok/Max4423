using BookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingSystem.Infrastructure.Data.Configurations;

public class ApartmentConfiguration:IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title).IsRequired();
        
        builder.Property(a => a.PricePerNight)
            .HasPrecision(18, 2);
        
        builder.HasOne(a => a.Host)
            .WithMany(u => u.Apartments)
            .HasForeignKey(a => a.HostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => a.ExternalId)
            .IsUnique();
        
        builder.Property(a => a.CustomData)
            .HasColumnType("jsonb");
    }
}
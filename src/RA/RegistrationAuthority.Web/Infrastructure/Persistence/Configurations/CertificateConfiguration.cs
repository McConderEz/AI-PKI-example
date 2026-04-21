using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationAuthority.Web.Domain.Entities;

namespace RegistrationAuthority.Web.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности сертификата.
/// </summary>
public sealed class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Certificate> builder)
    {
        builder.ToTable("certificates");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.SerialNumber).HasMaxLength(128).IsRequired();
        builder.Property(x => x.Subject).HasMaxLength(512).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(x => x.IssuedAt).IsRequired();
        builder.Property(x => x.ExpiresAt).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasIndex(x => x.SerialNumber).IsUnique();
        builder.HasIndex(x => x.CertRequestId).IsUnique();

        builder.HasOne(x => x.CertRequest)
            .WithOne(x => x.Certificate)
            .HasForeignKey<Certificate>(x => x.CertRequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

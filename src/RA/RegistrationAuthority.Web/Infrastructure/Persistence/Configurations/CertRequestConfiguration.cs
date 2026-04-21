using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationAuthority.Web.Domain.Entities;

namespace RegistrationAuthority.Web.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности заявки на выпуск сертификата.
/// </summary>
public sealed class CertRequestConfiguration : IEntityTypeConfiguration<CertRequest>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<CertRequest> builder)
    {
        builder.ToTable("cert_requests");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.CommonName).HasMaxLength(128).IsRequired();
        builder.Property(x => x.Subject).HasMaxLength(512).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.CertRequests)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationAuthority.Web.Domain.Entities;

namespace RegistrationAuthority.Web.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности заявки на отзыв сертификата.
/// </summary>
public sealed class RevRequestConfiguration : IEntityTypeConfiguration<RevRequest>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<RevRequest> builder)
    {
        builder.ToTable("rev_requests");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Reason).HasMaxLength(512).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.RevRequests)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Certificate)
            .WithMany(x => x.RevRequests)
            .HasForeignKey(x => x.CertificateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

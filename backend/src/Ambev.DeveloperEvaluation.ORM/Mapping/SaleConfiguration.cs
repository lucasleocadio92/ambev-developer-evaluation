using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleDate).IsRequired().HasColumnType("timestamp");
            builder.Property(s => s.Branch).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(20);

            builder.OwnsOne(s => s.Customer, customer =>
            {
                customer.Property(c => c.Name).HasColumnName("CustomerName").HasMaxLength(100);
                customer.Property(c => c.Email).HasColumnName("CustomerEmail").HasMaxLength(100);
                customer.Property(c => c.Phone).HasColumnName("CustomerPhone").HasMaxLength(20);
                customer.Property(c => c.Document).HasColumnName("CustomerDocument").HasMaxLength(20);
            });

            builder.HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey("SaleId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.ProductId);
            builder.Property(si => si.ProductId).HasColumnType("uuid");
            builder.Property(si => si.Quantity).IsRequired().HasColumnType("int");
            builder.Property(si => si.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(si => si.Discount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(si => si.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne<Sale>()
                .WithMany(s => s.Items)
                .HasForeignKey("SaleId");
        }
    }
}

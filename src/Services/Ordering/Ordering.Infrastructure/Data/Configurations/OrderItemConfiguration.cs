﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(id => id.Value, value => OrderItemId.Of(value))
                .IsRequired();

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired();
        }
    }
}

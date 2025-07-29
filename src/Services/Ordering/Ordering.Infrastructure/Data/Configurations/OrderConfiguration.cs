using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(id => id.Value, value => OrderId.Of(value));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasMany<OrderItem>()
                .WithOne()
                .HasForeignKey(x => x.OrderId);

            builder.ComplexProperty(x => x.OrderName, orderNameBuilder =>
            {
                orderNameBuilder.Property(x => x.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });

            builder.ComplexProperty(x => x.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(x => x.FirstName)
                    .HasMaxLength(150)
                    .IsRequired();
                addressBuilder.Property(x => x.LastName)
                    .HasMaxLength(100)
                    .IsRequired();
                addressBuilder.Property(x => x.EmailAddress)
                    .HasMaxLength(50);
                addressBuilder.Property(x => x.Zip)
                    .HasMaxLength(5)
                    .IsRequired();
                addressBuilder.Property(x => x.Country)
                    .HasMaxLength(50);
                addressBuilder.Property(x => x.State)
                    .HasMaxLength(50);
                addressBuilder.Property(x => x.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();
            });

            builder.ComplexProperty(x => x.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(x => x.FirstName)
                    .HasMaxLength(150)
                    .IsRequired();
                addressBuilder.Property(x => x.LastName)
                    .HasMaxLength(100)
                    .IsRequired();
                addressBuilder.Property(x => x.EmailAddress)
                    .HasMaxLength(50);
                addressBuilder.Property(x => x.Zip)
                    .HasMaxLength(5)
                    .IsRequired();
                addressBuilder.Property(x => x.Country)
                    .HasMaxLength(50);
                addressBuilder.Property(x => x.State)
                    .HasMaxLength(50);
                addressBuilder.Property(x => x.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();
            });

            builder.ComplexProperty(x => x.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(x => x.CardNumber)
                    .HasMaxLength(24);
                paymentBuilder.Property(x => x.CardName)
                    .HasMaxLength(50);
                paymentBuilder.Property(x => x.Expiration)
                    .HasMaxLength(10);
                paymentBuilder.Property(x => x.CVV)
                    .HasMaxLength(3);
                paymentBuilder.Property(x => x.PaymentMethod);
            });

            builder.Property(x => x.OrderStatus)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    status => status.ToString(),
                    value => Enum.Parse<OrderStatus>(value, true)
                );

            builder.Property(x => x.TotalPrice);
        }
    }
}

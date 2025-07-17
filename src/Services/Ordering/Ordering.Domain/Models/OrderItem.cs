using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<Guid>
    {
        internal OrderItem(Guid orderId,Guid productId,int quantity,decimal totalPrice)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            TotalPrice = totalPrice;
        }

        public Guid OrderId { get; private set; } = default!;
        public Guid ProductId { get; private set; } = default!;
        public int Quantity { get; private set; } = default!;
        public decimal TotalPrice { get; }
        public decimal Price { get; private set;} = default!;
    }
}

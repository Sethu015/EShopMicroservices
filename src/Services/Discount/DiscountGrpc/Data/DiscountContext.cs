using DiscountGrpc.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupones { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> options)
            : base(options)
        {
            
        }
    }
}

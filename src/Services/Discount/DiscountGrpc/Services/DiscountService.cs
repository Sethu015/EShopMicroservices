using DiscountGrpc.Data;
using DiscountGrpc.Grpc;
using DiscountGrpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Services
{
    public class DiscountService(ILogger<DiscountService> logger,DiscountContext discountContext) 
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var discount = await discountContext.Coupones.FirstOrDefaultAsync(x => x.ProductName.Equals(request.ProductName));
            if (discount is null)
            {
                discount = new Models.Coupon() { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            }
            logger.LogInformation("Coupon for Product {0} with Amount {1} is found.",discount.ProductName,discount.Amount);
            var couponModel = discount.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var couponModel = request.Coupon.Adapt<Coupon>();
            if (couponModel is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            var result = await discountContext.AddAsync(couponModel);
            await discountContext.SaveChangesAsync();
            logger.LogInformation("Created discount for Product {0} with Amount {1}",couponModel.ProductName,couponModel.Amount);
            var response = couponModel.Adapt<CouponModel>();
            return response;
        }

        public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            return base.UpdateDiscount(request, context);
        }

        public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return base.DeleteDiscount(request, context);
        }
    }
}

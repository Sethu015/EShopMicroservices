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

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            if(request is null || request.Coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument,"Invalid request object"));
            var existingCoupon = discountContext.Coupones.FirstOrDefault(x => x.Id == request.Coupon.Id);
            if (existingCoupon is null)
                throw new Exception($"Coupon Not found for Product {request.Coupon.ProductName}");
            existingCoupon.Amount = request.Coupon.Amount;
            existingCoupon.ProductName = request.Coupon.ProductName;
            existingCoupon.Description = request.Coupon.Description;
            var result = discountContext.Update(existingCoupon);
            await discountContext.SaveChangesAsync();
            var response = existingCoupon.Adapt<CouponModel>();
            return response;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            if (request is null || request.ProductName is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            var existingCoupon = discountContext.Coupones.FirstOrDefault(x => x.ProductName.Equals(request.ProductName));
            if (existingCoupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Product Name {request.ProductName} Not found"));
            discountContext.Remove(existingCoupon);
            await discountContext.SaveChangesAsync();
            return new DeleteDiscountResponse() { Success = true };
        }
    }
}

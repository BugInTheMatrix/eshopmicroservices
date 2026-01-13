
using Discount.grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) :ICommand<StoreBasketResult>;
    public record StoreBasketResult(string userName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotEmpty().NotNull().WithMessage("Cart cannt be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("username required");
        }
    }
    public class StoreBasketHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discount) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            await DeductDiscount(request.Cart, cancellationToken);
            ShoppingCart cart = await repository.StoreBasket(request.Cart, cancellationToken);
            return new StoreBasketResult(cart.UserName);
        }
        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            // Communicate with Discount.Grpc and calculate lastest prices of products into sc
            foreach (var item in cart.Items)
            {
                var coupon = await discount.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
}
    

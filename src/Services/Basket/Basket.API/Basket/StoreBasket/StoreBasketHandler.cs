
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
    public class StoreBasketHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            ShoppingCart cart = await repository.StoreBasket(request.Cart, cancellationToken);
            return new StoreBasketResult(cart.UserName);
        }
    }
}

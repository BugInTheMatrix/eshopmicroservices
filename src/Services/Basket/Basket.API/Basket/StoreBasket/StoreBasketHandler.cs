
using Basket.API.Data;
using FluentValidation;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart ShoppingCart):ICommand<StoreBasketResult>;
    public record StoreBasketResult(string userName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.ShoppingCart).NotEmpty().NotNull().WithMessage("Cart cannt be null");
            RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("username required");
        }
    }
    public class StoreBasketHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            ShoppingCart cart = await repository.StoreBasket(request.ShoppingCart,cancellationToken);
            return new StoreBasketResult(cart.UserName);
        }
    }
}

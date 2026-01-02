
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
    public class StoreBasketHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            ShoppingCart cart = request.ShoppingCart;
            return new StoreBasketResult("sw");
        }
    }
}

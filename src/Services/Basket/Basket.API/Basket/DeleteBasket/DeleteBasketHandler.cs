
using Basket.API.Data;
using FluentValidation;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string userName):ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool isSuccess);
    public class DeleteBasketCommandValidator:AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator() 
        {
            RuleFor(x => x.userName).NotEmpty().NotNull().WithMessage("username required");
        }
    }
    public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteBasket(request.userName, cancellationToken);
            return new DeleteBasketResult(true);
        }
    }
}

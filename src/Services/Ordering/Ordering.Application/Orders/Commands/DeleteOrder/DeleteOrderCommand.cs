using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(OrderDto Order) : ICommand<DeleteOrderResult>;
    public record DeleteOrderResult(bool IsDeleted);
    public class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand> 
    {
        public DeleteOrderValidator()
        {
                RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Order Id is required");
        }
    }
}


using Ordering.Application.Orders.Queries.GetOrdersByCustomers;

namespace Ordering.API.Endpoints
{
   // public record GetOrdersByCustomerRequest(string UserName);
    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var query = new GetOrdersByCustomerQuery(customerId);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersByCustomerResponse>();
                return Results.Ok(response);
            })
                .WithName("GetOrdersByCustomerId")
                .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Orders By Customer Id")
                .WithDescription("Get Orders By Customer Id");
        }
    }
}

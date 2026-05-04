
using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersByNameRequest(string UserName);
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/name/{orderName}", async (string orderName, ISender sender) =>
            {
                var query = new GetOrderByNameQuery(orderName);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersByNameResponse>();
                return Results.Ok(response);
            })
                .WithName("GetOrdersByUserName")
                .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Orders By User Name")
                .WithDescription("Get Orders By User Name");
        }
    }
}

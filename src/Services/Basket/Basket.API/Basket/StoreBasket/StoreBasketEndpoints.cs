
namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string userName);
    public class StoreBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/", async (StoreBasketRequest request, ISender sender) => 
            {
                var storeCommand = request.Adapt<StoreBasketRequest>();
                var result=sender.Send(storeCommand);
                var response = result.Adapt<StoreBasketResponse>();
                return Results.Created($"/basket/{response.userName}", response);

            })
            .WithName("CreateBasket")
            .Produces<StoreBasketCommand>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Basket")
            .WithDescription("Create Basket"); 
        }
    }
}

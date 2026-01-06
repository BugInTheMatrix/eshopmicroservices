
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
                var storeCommand = request.Adapt<StoreBasketCommand>();
                var result= await sender.Send(storeCommand);
                var response = new StoreBasketResponse(result.userName);
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

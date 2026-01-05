
using Basket.API.Basket.StoreBasket;

namespace Basket.API.Basket.DeleteBasket
{
    //public record DeleteBasketRequest(string userName);
    public record DeleteBasketResponse(bool isSuccess);
    public class DeleteBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}",async (string userName,ISender sender) =>
            {
                var reqestObject = new DeleteBasketCommand(userName);
                var result = await sender.Send(reqestObject);
                var response = result.Adapt<DeleteBasketResponse>();
                if (response.isSuccess)
                {
                    return Results.Ok(response);
                }
                else
                {
                    return Results.NotFound();
                }
            })
            .WithName("DeleteBasket")
            .Produces<StoreBasketCommand>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Basket")
            .WithDescription("Delete Basket");
            ;
        }
    }
}

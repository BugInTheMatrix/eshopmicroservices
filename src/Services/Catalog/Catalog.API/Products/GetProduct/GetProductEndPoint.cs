
namespace Catalog.API.Products.GetProduct
{
    //public record GetProductRequest();
    public record GetProductResponse(IEnumerable<Product>Products);
    public class GetProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products", async (ISender sender) =>
            {
                var query = new GetProductQuery();
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
        }
    }
}

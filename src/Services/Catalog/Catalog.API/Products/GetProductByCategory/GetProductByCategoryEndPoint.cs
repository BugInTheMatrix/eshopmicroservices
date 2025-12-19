
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products/Category/{Category}", async (string Category, ISender Sender) =>
            {
                var result= await Sender.Send(new GetProductByCategoryQuery(Category));
                return Results.Ok(result.Adapt<GetProductByCategoryResponse>());

            }).WithName("GetProductbyCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by Category")
            .WithDescription("Get Product by Category");
        }
    }
}

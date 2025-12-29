
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCategories
{
    public record GetProductByCategoriesResponse(IEnumerable<Product> Products);
    public class GetProductByCategoriesEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products/Categories/{Categories}", async (string Categories, ISender Sender) =>
            {
                var result= await Sender.Send(new GetProductByCategoriesQuery(Categories));
                return Results.Ok(result.Adapt<GetProductByCategoriesResponse>());

            }).WithName("GetProductbyCategories")
            .Produces<GetProductByCategoriesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by Categories")
            .WithDescription("Get Product by Categories");
        }
    }
}


using Catalog.API.Products.GetProduct;

namespace Catalog.API.Products.GetProductById
{
    //public record GetProductByIdRequest(Guid Id);
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products/{id}", async (Guid id, ISender sender) =>
            {
                var result=await sender.Send(new GetProductByIdQuery(id));
                var response = new GetProductByIdResponse(new Product
                {
                    Id = result.Id,
                    Name = result.Name,
                    Categories = result.Categories,
                    Description = result.Description,
                    ImageFile = result.ImageFile,
                    Price = result.Price
                });
                return Results.Ok(response);

            })
            .WithName("GetProductbyid")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by id")
            .WithDescription("Get Product by id");
        }
    }
}



namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price);
    internal class GetProductByIdQueryHandler(IDocumentSession session,ILogger logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductByIdRequest for Id: {Id}", query.Id);
            var result= await session.LoadAsync<Product>(query.Id, cancellationToken);
            if (result == null)
            {
                throw new ProductNotFound();
            }
            return new GetProductByIdResult(result.Id, result.Name, result.Categories, result.Description, result.ImageFile, result.Price);
        }
    }
}


namespace Catalog.API.Products.GetProductByCategories
{
    public record GetProductByCategoriesQuery(string Categories): IQuery<GetProductByCategoriesResult>;
    public record GetProductByCategoriesResult(IEnumerable<Product> Products);
    internal class GetProductByCategoriesQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoriesQuery, GetProductByCategoriesResult>
    {
        public async Task<GetProductByCategoriesResult> Handle(GetProductByCategoriesQuery query, CancellationToken cancellationToken)
        {
            var result = await session.Query<Product>()
                .Where(p => p.Categories.Contains(query.Categories))
                .ToListAsync(cancellationToken);
            return new GetProductByCategoriesResult(result);
        }
    }
}

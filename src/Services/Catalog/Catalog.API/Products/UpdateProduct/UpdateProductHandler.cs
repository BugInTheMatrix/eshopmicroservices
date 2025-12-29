using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductQuery(Guid Id,string Name, List<string> Categories, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductQuery, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
            if (product == null)
            {
                return new UpdateProductResult(false);
            }
            product.Name = query.Name;
            product.Categories = query.Categories;
            product.Description = query.Description;
            product.ImageFile = query.ImageFile;
            product.Price = query.Price;
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);

        }
    }
}

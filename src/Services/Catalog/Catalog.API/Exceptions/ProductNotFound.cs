using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions
{
    public class ProductNotFound: NotFoundException
    {
        public ProductNotFound(Guid Id) : base("Product", Id)
        {
        }
    }
}

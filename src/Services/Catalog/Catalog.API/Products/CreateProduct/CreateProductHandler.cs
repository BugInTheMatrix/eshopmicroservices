using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string name,List<string> Categories,string Description,string ImageFile,string Price)
        :IRequest<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

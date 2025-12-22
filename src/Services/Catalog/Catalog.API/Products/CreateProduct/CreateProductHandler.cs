using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name,List<string> Categories,string Description,string ImageFile,decimal Price)
        :ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandValidater : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidater()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Name is required");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("category is required");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500).WithMessage("descri[tion is required"); ;
            RuleFor(x => x.ImageFile).NotEmpty().MaximumLength(200).WithMessage("imagefile is required"); ;
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("price is required"); ;
        }
    }
    public class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            

            Product product = new Product()
            {
                Name=command.Name,
                Categories=command.Categories,
                Description=command.Description,
                ImageFile=command.ImageFile,
                Price=command.Price

            };
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}

using BuildingBlocks.CQRS;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string name, List<string> Category, string description, string imageFile, decimal price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid id);
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //Business logic to create a product
            throw new NotImplementedException();
        }
    }
}

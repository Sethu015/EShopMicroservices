
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductsCommand(Guid id, string name, List<string> category, string description, string imageFile, decimal price)
        :ICommand<UpdateProductsResult>;
    public record UpdateProductsResult(bool isSuccess);

    public class UpdateProductsCommandValidator : AbstractValidator<UpdateProductsCommand>
    {
        public UpdateProductsCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id is required!");

            RuleFor(x => x.name).NotEmpty().WithMessage("Name is Required")
                .Length(2, 150).WithMessage("Name must be minimum 2 charecters and must not exceed 150 chrecters");

            RuleFor(x => x.price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    public class UpdateProductsCommandHandler(IDocumentSession session,ILogger<UpdateProductsCommandHandler> logger) : ICommandHandler<UpdateProductsCommand, UpdateProductsResult>
    {
        public async Task<UpdateProductsResult> Handle(UpdateProductsCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Executing UpdateProductsCommandHandler.Handle Method with Command {0}", command);
            var existingProduct = await session.LoadAsync<Product>(command.id, cancellationToken);
            if (existingProduct is null)
            {
                throw new ProductNotFoundException(command.id);
            }
            existingProduct.Name = command.name;
            existingProduct.Category = command.category;
            existingProduct.Description = command.description;
            existingProduct.ImageFile = command.imageFile;
            existingProduct.Price = command.price;

            session.Update(existingProduct);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductsResult(isSuccess : true);
        }
    }
}

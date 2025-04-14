namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string name, List<string> category, string description, string imageFile, decimal price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.imageFile).NotEmpty().WithMessage("Image file is required");
            RuleFor(x => x.price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    public class CreateProductCommandHandler (IDocumentSession session,ILogger<CreateProductCommandHandler> logger)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Executing CreateProductCommandHandler.Handle method with comman {0}", command);

            //Create Product Entity from command
            //save in database
            //return cancellationproductresult

            var product = new Product()
            {
                Name = command.name,
                Category = command.category,
                Description = command.description,
                ImageFile = command.imageFile,
                Price = command.price
            };

            //TODO
            //save to db
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            //return result

            return new CreateProductResult(product.Id);
        }
    }
}

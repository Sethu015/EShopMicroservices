namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string name, List<string> category, string description, string imageFile, decimal price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid id);
    public class CreateProductCommandHandler (IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
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

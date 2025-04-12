
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductsCommand(Guid id, string name, List<string> category, string description, string imageFile, decimal price)
        :ICommand<UpdateProductsResult>;
    public record UpdateProductsResult(bool isSuccess);
    public class UpdateProductsCommandHandler(IDocumentSession session,ILogger<UpdateProductsCommandHandler> logger) : ICommandHandler<UpdateProductsCommand, UpdateProductsResult>
    {
        public async Task<UpdateProductsResult> Handle(UpdateProductsCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Executing UpdateProductsCommandHandler.Handle Method with Command {0}", command);
            var existingProduct = await session.LoadAsync<Product>(command.id, cancellationToken);
            if (existingProduct is null)
            {
                throw new ProductNotFoundException();
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

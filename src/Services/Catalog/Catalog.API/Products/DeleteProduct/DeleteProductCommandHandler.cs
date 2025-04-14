
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool isSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id is required!");
        }
    }

    public class DeleteProductCommandHandler(IDocumentSession session,ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Executing DeleteProductCommandHandler.Handle method with command {0}", command);

            session.Delete<Product>(command.id);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}

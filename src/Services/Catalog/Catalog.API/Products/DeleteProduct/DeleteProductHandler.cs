using BuildingBlocks.SQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using System.Windows.Input;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductResult(bool IsSuccess);
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    internal class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductHandler.Handle called by {@Command}", command);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null) 
            {
                throw new ProductNotFoundException();
            }

            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}

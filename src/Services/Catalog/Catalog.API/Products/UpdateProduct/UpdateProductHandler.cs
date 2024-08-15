using BuildingBlocks.SQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten.Linq.SoftDeletes;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductResult(bool IsSuccess);
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;
    public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler.Handle called by {@Command}", command);
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException();
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.Price = command.Price;
            product.ImageFile = command.ImageFile;

            session.Update(product);
            session.SaveChanges();

            return new UpdateProductResult(true);
        }
    }
}


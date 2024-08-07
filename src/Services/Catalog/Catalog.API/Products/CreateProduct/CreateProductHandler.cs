using BuildingBlocks.SQRS;
using Catalog.API.Models;


namespace Catalog.API.Products.CreateProduct;

public record CreateProductComand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductComand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductComand request, CancellationToken cancellationToken)
    {
        var product = request.Adapt<Product>();
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}


using BuildingBlocks.SQRS;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductComand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler : ICommandHandler<CreateProductComand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductComand request, CancellationToken cancellationToken)
    {
        return new CreateProductResult(Guid.NewGuid());
    }
}


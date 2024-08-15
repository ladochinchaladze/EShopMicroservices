using BuildingBlocks.SQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsResult(IEnumerable<Product> Products);
    public record GetProductsQuery() : IQuery<GetProductsResult>;

    internal class GetProductsHandler(IDocumentSession session, ILogger<GetProductsHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsHandler.Handle Called with {@Query}", query);
            var products = await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductsResult(products);
        }
    }
}

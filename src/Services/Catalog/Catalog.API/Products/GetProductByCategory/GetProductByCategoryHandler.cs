using BuildingBlocks.SQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    internal class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryHandler.Handle called with {@Query}", query);

            var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }

}

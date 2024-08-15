using Catalog.API.Models;
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/all", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            }).WithName("GetProducts")
             .Produces<GetProductsResponse>(StatusCodes.Status200OK)
             .WithSummary("Get Products")
             .WithDescription("Get Products"); ;
        }
    }
}

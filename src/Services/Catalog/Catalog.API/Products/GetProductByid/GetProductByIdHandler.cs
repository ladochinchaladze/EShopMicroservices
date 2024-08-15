﻿using BuildingBlocks.SQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;

namespace Catalog.API.Products.GetProductByid
{
    public record GetProductByIdResult(Product Product);
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    internal class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger) :
        IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdHandler.Handle called with {@Query}", query);

            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException();
            }

            return new GetProductByIdResult(product);
        }
    }
}

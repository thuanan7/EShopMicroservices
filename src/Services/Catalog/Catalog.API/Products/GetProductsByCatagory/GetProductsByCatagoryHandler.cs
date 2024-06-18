﻿using System.Linq;

namespace Catalog.API.Products.GetProductsByCatagory
{
    public record GetProductsByCatagoryQuery(string Catagory) : IQuery<GetProductsByCatagoryResult>;
    public record GetProductsByCatagoryResult(IEnumerable<Product> Products);
    internal class GetProductsByCatagoryQueryHandler(IDocumentSession session, ILogger<GetProductsByCatagoryQueryHandler> logger)
        : IQueryHandler<GetProductsByCatagoryQuery, GetProductsByCatagoryResult>
    {
        public async Task<GetProductsByCatagoryResult> Handle(GetProductsByCatagoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsByCatagoryQueryHandler.Handle called with {@Query}", query);
            var products = await session.Query<Product>()
                .Where(p => p.Catagory.Contains(query.Catagory)).ToListAsync(cancellationToken);

            return new GetProductsByCatagoryResult(products);
        }
    }
}

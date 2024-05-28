namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQuery> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query);
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null) throw new ProductNotFoundException();
        var response = product.Adapt<Product>();

        return new GetProductByIdResult(response);
    }
}
using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);
internal class DeleteProductCommandHandler
    (IDocumentSession session,ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting product {Id}", request.Id);
        var product = await session.LoadAsync<Product>(request.Id);
        if (product is null)
        {
            logger.LogWarning("Product {Id} not found for deletion", request.Id);
            throw new ProductNotFoundException(request.Id);
        }
        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Product {Id} deleted successfully", request.Id);
        return new DeleteProductResult(true);
    }
}
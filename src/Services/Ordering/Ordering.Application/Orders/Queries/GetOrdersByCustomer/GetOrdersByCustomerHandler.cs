namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;
public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        // Simple query to get all orders for a specific customer
        var orders = await dbContext.Orders
            .AsNoTracking()
            .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
            .ToListAsync(cancellationToken);

        // Convert to DTOs
        var orderDtos = orders.ToOrderDtoList();

        // Return result
        return new GetOrdersByCustomerResult(orderDtos);


    }
}
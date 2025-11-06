namespace Ordering.Application.Orders.Queries.GetOrdersByName;
public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        // Handle null or empty name
        if (string.IsNullOrWhiteSpace(query.Name))
        {
            return new GetOrdersByNameResult(new List<OrderDto>());
        }

        var orders = await dbContext.Orders
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Contains(query.Name))
            .ToListAsync(cancellationToken);

        return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }
}
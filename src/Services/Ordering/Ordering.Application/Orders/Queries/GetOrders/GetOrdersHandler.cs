namespace Ordering.Application.Orders.Queries.GetOrders;
public class GetOrdersHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {

        int pageNumber = query.PaginationRequest.PageIndex;
        int pageSize = query.PaginationRequest.PageSize;


        var orders = await dbContext.Orders
            .AsNoTracking()
            .ToListAsync(cancellationToken);


        return new GetOrdersResult(
            new PaginatedResult<OrderDto>(pageNumber, pageSize, orders.Count, orders.ToOrderDtoList()));
    }
}
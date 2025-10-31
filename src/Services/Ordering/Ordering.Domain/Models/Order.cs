using Ordering.Domain.Abstraction;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }

    public static Order Create(OrderId id,CustomerId customerId, OrderName orderName, Address shippingAddress, Payment payment, OrderStatus status)
    {
       var Order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = shippingAddress,
            Payment = payment,
            Status = status
        };

        return Order;
    }
}
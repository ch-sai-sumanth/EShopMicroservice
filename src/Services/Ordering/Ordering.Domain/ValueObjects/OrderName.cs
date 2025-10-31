namespace Ordering.Domain.ValueObjects;

public class OrderName
{
    public string Value { get; }


    private OrderName(string value)=> Value = value;

    public static OrderName Of(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentException.ThrowIfNullOrEmpty(value);

        return new OrderName(value);
    }
}
namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(CheckoutBasketDto checkoutBasketDto)
    : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccessful);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.checkoutBasketDto).NotNull().WithMessage("CheckoutBasketDto can't be null.");
        RuleFor(x=>x.checkoutBasketDto.UserName).NotEmpty().WithMessage("UserName is required.");
    }
}
public class CheckoutBasketHandler
    (IBasketRepository repository)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        // Here you can add logic to process the checkout, like publishing an event to an event bus
        return new CheckoutBasketResult(true);
    }
}
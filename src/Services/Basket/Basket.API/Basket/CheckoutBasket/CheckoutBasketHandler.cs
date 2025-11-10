using BuildingBlocks.messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(CheckoutBasketDto CheckoutBasketDto)
    : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccessful);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.CheckoutBasketDto).NotNull().WithMessage("CheckoutBasketDto can't be null.");
        RuleFor(x=>x.CheckoutBasketDto.UserName).NotEmpty().WithMessage("UserName is required.");
    }
}
public class CheckoutBasketHandler
    (IBasketRepository repository,IPublishEndpoint publishEndpoint,ILogger<CheckoutBasketHandler> logger)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        // get existing basket with total price
        // Set totalprice on basketcheckout event message
        // send basket checkout event to rabbitmq using masstransit
        // delete the basket
        logger.LogInformation("Checkout initiated for {UserName}", command.CheckoutBasketDto?.UserName);


        var basket = await repository.GetBasket(command.CheckoutBasketDto.UserName, cancellationToken);
        if (basket == null)
        {
            return new CheckoutBasketResult(false);
        }

        // Here you would typically publish an event to a message broker
        // For example, using MassTransit to publish a BasketCheckoutEvent
        var eventMessage = command.CheckoutBasketDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasket(command.CheckoutBasketDto.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}
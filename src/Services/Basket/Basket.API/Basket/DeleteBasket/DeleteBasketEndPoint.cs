namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketRequest(string UserName);
public record DeleteBasketResponse(bool IsSuccess);
public class DeleteBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var command = new DeleteBasketCommand(userName);

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);
        })
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Deletes the shopping basket for a user")
            .WithDescription("Deletes the shopping basket associated with the specified user.");
    }
}
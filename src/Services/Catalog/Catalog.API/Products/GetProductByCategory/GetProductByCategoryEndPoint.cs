namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByIdResponse(IEnumerable<Product> Products);
public class GetProductByCategoryEndPoint :ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var query = new GetProductByCategoryQuery(category);

            var result = await sender.Send(query);

            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        });
    }
}
using AdventureAPI.UseCases.Stores.Get;

namespace AdventureAPI.Web.Controllers.Stores;

public class GetById(IMediator mediator)
    : Endpoint<GetStoreByIdRequest, GetStoreByIdResponse>
{
    public override void Configure()
    {
        Get(GetStoreByIdRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetStoreByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetStoreQuery(request.StoreId);
        var result = await mediator.Send(query, cancellationToken);
        var response = new GetStoreByIdResponse(result.Value, result.SuccessMessage);
        await SendOkAsync(response, cancellationToken);
    }
}

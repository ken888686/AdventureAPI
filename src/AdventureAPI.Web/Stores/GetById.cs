using AdventureAPI.UseCases.Stores.Get;
using AdventureAPI.Web.Responses;

namespace AdventureAPI.Web.Stores;

public class GetById(IMediator mediator)
    : Endpoint<GetStoreByIdRequest>
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
        if (result.Status == ResultStatus.NotFound)
        {
            var response = new NotFoundResponse(result.Errors);
            await SendOkAsync(response, cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            var response = new GetStoreByIdResponse(result.Value, result.SuccessMessage);
            await SendOkAsync(response, cancellationToken);
        }
    }
}

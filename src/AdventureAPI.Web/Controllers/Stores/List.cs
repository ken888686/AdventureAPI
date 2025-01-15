using AdventureAPI.UseCases.Stores.List;

namespace AdventureAPI.Web.Controllers.Stores;

public class List(IMediator mediator) : Endpoint<StoreListRequest, StoreListResponse>
{
    public override void Configure()
    {
        Get(StoreListRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        StoreListRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new ListStoresQuery(request.Skip, request.Take),
            cancellationToken);
        if (result.IsSuccess)
        {
            await SendOkAsync(
                new StoreListResponse(
                    result.Value.Select(
                        x => new StoreRecord(
                            x.Id,
                            x.Name,
                            x.Address,
                            x.Logo,
                            x.Status))),
                cancellationToken);
        }
    }
}

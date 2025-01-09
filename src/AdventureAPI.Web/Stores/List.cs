using AdventureAPI.UseCases.Stores.List;

namespace AdventureAPI.Web.Stores;

public class List(IMediator mediator) : EndpointWithoutRequest<StoreListResponse>
{
    public override void Configure()
    {
        Get(StoreListRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ListStoresQuery(null, null), cancellationToken);
        if (result.IsSuccess)
        {
            Response = new StoreListResponse(
                result.Value.Select(
                    x => new StoreRecord(
                        x.Id,
                        x.Name,
                        x.Address,
                        x.Logo,
                        x.Status)));
        }
    }
}

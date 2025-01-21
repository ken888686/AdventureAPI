using AdventureAPI.UseCases.Stores.List;

namespace AdventureAPI.Web.Controllers.Stores;

public class List(IMediator mediator)
    : Endpoint<StoreListRequest, StoreListResponse>
{
    public override void Configure()
    {
        Get(StoreListRequest.Route);
        AllowAnonymous();
        Summary(
            s =>
            {
                s.ExampleRequest = new StoreListRequest { Skip = 10, Take = 10 };
            });
    }

    public override async Task HandleAsync(StoreListRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new ListStoresQuery(request.Skip, request.Take),
            cancellationToken);

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

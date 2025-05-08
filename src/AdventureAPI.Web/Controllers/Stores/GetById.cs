using AdventureAPI.UseCases.Stores.Get;

namespace AdventureAPI.Web.Controllers.Stores;

public class GetById(IMediator mediator) : Endpoint<GetStoreByIdRequest, GetStoreByIdResponse>
{
    public override void Configure()
    {
        Get(GetStoreByIdRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new GetStoreByIdRequest { StoreId = Guid.NewGuid() };
        });
    }

    public override async Task HandleAsync(
        GetStoreByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var query = new GetStoreQuery(request.StoreId);
        var result = await mediator.Send(query, cancellationToken);
        Response = new GetStoreByIdResponse(
            new StoreRecord(
                result.Value.Id,
                result.Value.Name,
                result.Value.Address,
                result.Value.Logo,
                result.Value.Status
            )
        );
    }
}

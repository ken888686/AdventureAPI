using AdventureAPI.UseCases.Cities.List;

namespace AdventureAPI.Web.Controllers.Cities;

public class List(IMediator mediator) : Endpoint<CityListRequest, CityListResponse>
{
    public override void Configure()
    {
        Get(CityListRequest.Route);
        AllowAnonymous();
        Summary(
            s =>
            {
                s.ExampleRequest = new CityListRequest { Skip = 10, Take = 10 };
            });
    }

    public override async Task HandleAsync(
        CityListRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new ListCitiesQuery(request.Skip, request.Take),
            cancellationToken);
        if (result.IsSuccess)
        {
            Response = new CityListResponse(
                result.Value.Select(
                    x => new CityRecord(x.Id, x.Name)
                )
            );
        }
    }
}

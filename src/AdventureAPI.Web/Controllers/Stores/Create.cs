using System.Net;
using AdventureAPI.Core.ValueObjects;
using AdventureAPI.UseCases.Stores;
using AdventureAPI.UseCases.Stores.Create;

namespace AdventureAPI.Web.Controllers.Stores;

public class Create(IMediator mediator)
    : Endpoint<CreateStoreRequest, CreateStoreResponse>
{
    public override void Configure()
    {
        Post(CreateStoreRequest.Route);
        AllowAnonymous();
        Summary(
            s =>
            {
                // XML Docs are used by default but are overridden by these properties:
                s.ExampleRequest = new CreateStoreRequest
                {
                    Name = "Store Name",
                    Address = new Address(
                        "100-0005",
                        "Tokyo",
                        "Chiyoda",
                        "Marunouchi",
                        "1 Chome",
                        "",
                        "",
                        139.749466,
                        35.686958
                    )
                };
            });
    }

    public override async Task HandleAsync(CreateStoreRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new CreateStoreCommand(
                request.Name,
                request.Address,
                "Test User"),
            cancellationToken);

        if (result.IsSuccess)
        {
            var response = new CreateStoreResponse(
                new StoreDto(
                    result.Value.Id,
                    result.Value.Name,
                    result.Value.Address,
                    result.Value.Logo,
                    result.Value.Status),
                result.SuccessMessage);

            await SendAsync(
                response,
                (int)HttpStatusCode.Created,
                cancellationToken);
        }
    }
}

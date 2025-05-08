using AdventureAPI.Core.Aggregates.UserAggregate;
using AdventureAPI.Core.ValueObjects;
using AdventureAPI.UseCases.Stores;
using AdventureAPI.UseCases.Stores.Create;

namespace AdventureAPI.Web.Controllers.Stores;

public class Create(IMediator mediator) : Endpoint<CreateStoreRequest, CreateStoreResponse>
{
    public override void Configure()
    {
        Post(CreateStoreRequest.Route);
        Roles(
            UserRole.SuperAdmin.ToString(),
            UserRole.Admin.ToString(),
            UserRole.ApiUser.ToString()
        );
        Summary(s =>
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
                ),
            };
        });
    }

    public override async Task HandleAsync(
        CreateStoreRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(
            new CreateStoreCommand(request.Name, request.Address, "Test User"),
            cancellationToken
        );

        if (result.IsSuccess)
        {
            Response = new CreateStoreResponse(
                new StoreDto(
                    result.Value.Id,
                    result.Value.Name,
                    result.Value.Address,
                    result.Value.Logo,
                    result.Value.Status
                )
            );
            return;
        }

        Response = result.Status switch
        {
            ResultStatus.NotFound => new CreateStoreResponse(
                null,
                StatusCodes.Status404NotFound,
                result.Errors
            ),
            ResultStatus.Invalid => new CreateStoreResponse(
                null,
                StatusCodes.Status400BadRequest,
                result.ValidationErrors.Select(x => x.ErrorMessage)
            ),
            _ => new CreateStoreResponse(
                null,
                StatusCodes.Status500InternalServerError,
                result.Errors
            ),
        };
    }
}

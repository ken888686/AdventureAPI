using AdventureAPI.UseCases.Auth.Register;

namespace AdventureAPI.Web.Controllers.Auth;

public class Register(IMediator mediator) : Endpoint<RegisterRequest, RegisterResponse>
{
    public override void Configure()
    {
        Post(RegisterRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        RegisterRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(
            new RegisterCommand(
                request.Username,
                request.Email,
                request.Password,
                request.FirstName,
                request.LastName
            ),
            cancellationToken
        );

        if (result.IsSuccess)
        {
            Response = new RegisterResponse(result.Value);
        }
    }
}

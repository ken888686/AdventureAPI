using AdventureAPI.UseCases.Auth.Login;

namespace AdventureAPI.Web.Controllers.Auth;

public class Login(IMediator mediator)
    : Endpoint<LoginRequest, LoginResponse>
{
    public override void Configure()
    {
        Post(LoginRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new LoginCommand(request.Username, request.Password),
            cancellationToken);

        if (result.IsSuccess)
        {
            Response = new LoginResponse(result.Value);
            return;
        }

        Response = result.Status switch
        {
            ResultStatus.NotFound => new LoginResponse(string.Empty, StatusCodes.Status404NotFound, result.Errors),
            ResultStatus.Invalid => new LoginResponse(
                string.Empty,
                StatusCodes.Status400BadRequest,
                result.ValidationErrors.Select(x => x.ErrorMessage)),
            _ => new LoginResponse(string.Empty, StatusCodes.Status500InternalServerError, result.Errors)
        };
    }
}

using AdventureAPI.Core.Aggregates.UserAggregate;
using AdventureAPI.UseCases.Users.Get;

namespace AdventureAPI.Web.Controllers.Users;

public class GetById(IMediator mediator)
    : Endpoint<GetUserByIdRequest, GetUserByIdResponse>
{
    public override void Configure()
    {
        Get(GetUserByIdRequest.Route);
        Roles(UserRole.SuperAdmin.ToString(), UserRole.Admin.ToString(), UserRole.ApiUser.ToString());
        Summary(
            s =>
            {
                s.ExampleRequest = new GetUserByIdRequest { UserId = Guid.NewGuid() };
            });
    }

    public override async Task HandleAsync(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var query = new GetUserInfoQuery(request.UserId);
        var result = await mediator.Send(query, cancellationToken);
        Response = new GetUserByIdResponse(
            new UserRecord(
                result.Value.UserId,
                result.Value.Username,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName,
                result.Value.PhotoUrl));
    }
}

using AdventureAPI.Web.Validators;
using FluentValidation;

namespace AdventureAPI.Web.Controllers.Users;

public class GetUserByIdValidator : Validator<GetUserByIdRequest>
{
    public GetUserByIdValidator()
    {
        RuleFor(x => x.UserId.ToString())
            .NotEmpty()
            .WithMessage("Id is required")
            .Must(CustomValidators.BeAValidGuid)
            .WithMessage("Id must be a valid GUID");
    }
}

using AdventureAPI.Web.Validators;
using FluentValidation;

namespace AdventureAPI.Web.Controllers.Stores;

public class GetStoreValidator : Validator<GetStoreByIdRequest>
{
    public GetStoreValidator()
    {
        RuleFor(x => x.StoreId.ToString())
            .NotEmpty()
            .WithMessage("Id is required")
            .Must(CustomValidators.BeAValidGuid)
            .WithMessage("Id must be a valid GUID");
    }
}

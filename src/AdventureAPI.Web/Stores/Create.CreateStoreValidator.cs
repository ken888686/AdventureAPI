using FluentValidation;

namespace AdventureAPI.Web.Stores;

public class CreateStoreValidator : Validator<CreateStoreRequest>
{
    public CreateStoreValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Address)
            .NotNull()
            .WithMessage("Address is required");
    }
}

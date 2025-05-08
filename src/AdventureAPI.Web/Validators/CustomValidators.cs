namespace AdventureAPI.Web.Validators;

public static class CustomValidators
{
    public static bool BeAValidGuid(string value)
    {
        return Guid.TryParse(value, out _);
    }
}

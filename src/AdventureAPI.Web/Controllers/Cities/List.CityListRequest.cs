namespace AdventureAPI.Web.Controllers.Cities;

public class CityListRequest
{
    public const string Route = "/cities";
    public int? Skip { get; init; }
    public int? Take { get; init; }
}

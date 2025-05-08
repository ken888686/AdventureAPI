namespace AdventureAPI.Web.Controllers.Stores;

public class StoreListRequest
{
    public const string Route = "/stores";
    public int? Skip { get; init; }
    public int? Take { get; init; }
}

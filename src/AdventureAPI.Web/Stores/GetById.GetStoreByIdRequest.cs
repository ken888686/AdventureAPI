namespace AdventureAPI.Web.Stores;

public class GetStoreByIdRequest
{
    public const string Route = "/stores/{StoreId:Guid}";

    public required Guid StoreId { get; set; }

    public static string BuildRoute(int storeId)
    {
        return Route.Replace("{StoreId:Guid}", storeId.ToString());
    }
}

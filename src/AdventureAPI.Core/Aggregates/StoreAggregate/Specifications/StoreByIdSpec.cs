namespace AdventureAPI.Core.Aggregates.StoreAggregate.Specifications;

public class StoreByIdSpec : Specification<Store>
{
    public StoreByIdSpec(Guid storeId)
    {
        Query.Where(store => store.Id == storeId);
    }
}

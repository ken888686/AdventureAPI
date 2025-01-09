namespace AdventureAPI.Core.Aggregates.StoreAggregate.Specifications;

public sealed class StoreByIdSpec : Specification<Store>
{
    public StoreByIdSpec(Guid storeId)
    {
        Query.Where(store => store.Id == storeId);
    }
}

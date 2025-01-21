namespace AdventureAPI.Core.Aggregates.StoreAggregate.Specifications;

public class StoreByNameSpec : Specification<Store>
{
    public StoreByNameSpec(string name)
    {
        Query.Where(store => store.Name == name);
    }
}

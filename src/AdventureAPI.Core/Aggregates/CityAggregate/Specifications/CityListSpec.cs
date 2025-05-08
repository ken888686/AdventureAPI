using AdventureAPI.Core.Constants;

namespace AdventureAPI.Core.Aggregates.CityAggregate.Specifications;

public sealed class CityListSpec : Specification<City>
{
    public CityListSpec(int? skip, int? take)
    {
        Query.Skip(skip ?? 0).Take(take ?? Pagination.DEFAULT_PAGE_SIZE);
    }
}

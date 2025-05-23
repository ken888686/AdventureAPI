﻿using AdventureAPI.Core.Constants;

namespace AdventureAPI.Core.Aggregates.StoreAggregate.Specifications;

public sealed class StoreListSpec : Specification<Store>
{
    public StoreListSpec(int? skip, int? take)
    {
        Query.Skip(skip ?? 0).Take(take ?? Pagination.DEFAULT_PAGE_SIZE);
    }
}

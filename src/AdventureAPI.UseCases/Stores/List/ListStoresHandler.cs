using AdventureAPI.Core.Aggregates.StoreAggregate;
using AdventureAPI.Core.Aggregates.StoreAggregate.Specifications;
using Microsoft.Extensions.Caching.Memory;

namespace AdventureAPI.UseCases.Stores.List;

public class ListStoresHandler(IRepository<Store> repository, IMemoryCache cache)
    : IQueryHandler<ListStoresQuery, Result<IEnumerable<StoreDto>>>
{
    private const string CacheKeyPrefix = "stores_list_";
    private static readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    public async Task<Result<IEnumerable<StoreDto>>> Handle(
        ListStoresQuery request,
        CancellationToken cancellationToken
    )
    {
        var cacheKey = $"{CacheKeyPrefix}{request.Skip}_{request.Take}";

        if (cache.TryGetValue(cacheKey, out IEnumerable<StoreDto>? cachedResult))
        {
            return Result<IEnumerable<StoreDto>>.Success(cachedResult!);
        }

        var spec = new StoreListSpec(request.Skip, request.Take);
        var stores = await repository.ListAsync(spec, cancellationToken);
        var result = stores.Select(x => new StoreDto(x.Id, x.Name, x.Address, x.Logo, x.Status));

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(_cacheDuration)
            .SetPriority(CacheItemPriority.Normal);

        cache.Set(cacheKey, result, cacheOptions);

        return Result<IEnumerable<StoreDto>>.Success(result);
    }
}

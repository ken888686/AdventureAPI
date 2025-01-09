using AdventureAPI.Core.Aggregates.StoreAggregate;
using AdventureAPI.Core.Aggregates.StoreAggregate.Specifications;

namespace AdventureAPI.UseCases.Stores.List;

public class ListStoresHandler(IRepository<Store> repository)
    : IQueryHandler<ListStoresQuery, Result<IEnumerable<StoreDto>>>
{
    public async Task<Result<IEnumerable<StoreDto>>> Handle(
        ListStoresQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new StoreListSpec(request.Skip, request.Take);
        var stores = await repository.ListAsync(spec, cancellationToken);
        return new Result<IEnumerable<StoreDto>>(
            stores.Select(x => new StoreDto(x.Id, x.Name, x.Address, x.Logo, x.Status))
        );
    }
}

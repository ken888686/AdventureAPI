using AdventureAPI.Core.Aggregates.StoreAggregate;
using AdventureAPI.Core.Aggregates.StoreAggregate.Specifications;

namespace AdventureAPI.UseCases.Stores.Get;

public class GetStoreHandler(IReadRepository<Store> repository)
    : IQueryHandler<GetStoreQuery, Result<StoreDto>>
{
    public async Task<Result<StoreDto>> Handle(GetStoreQuery request, CancellationToken cancellationToken)
    {
        var spec = new StoreByIdSpec(request.StoreId);
        var entity = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (entity is null)
        {
            return Result<StoreDto>.NotFound("Store not found");
        }

        return Result<StoreDto>.Success(
            new StoreDto(
                entity.Id,
                entity.Name,
                entity.Address,
                entity.Logo,
                entity.Status));
    }
}

using AdventureAPI.Core.Aggregates.StoreAggregate;
using AdventureAPI.Core.Aggregates.StoreAggregate.Specifications;

namespace AdventureAPI.UseCases.Stores.Create;

public class CreateStoreHandler(IRepository<Store> repository)
    : ICommandHandler<CreateStoreCommand, Result<StoreDto>>
{
    public async Task<Result<StoreDto>> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        // Check if the store name exists
        var spec = new StoreByNameSpec(request.Name);
        var exists = await repository.AnyAsync(spec, cancellationToken);
        if (exists)
        {
            return Result<StoreDto>.Conflict($"{request.Name} already exists.");
        }

        var newStore = new Store(request.Name, request.CreateUser, request.Logo);
        newStore.UpdateAddress(request.Address, request.CreateUser);
        var createdItem = await repository.AddAsync(newStore, cancellationToken);
        return Result<StoreDto>.Created(
            new StoreDto(
                newStore.Id,
                newStore.Name,
                newStore.Address,
                newStore.Logo,
                newStore.Status));
    }
}

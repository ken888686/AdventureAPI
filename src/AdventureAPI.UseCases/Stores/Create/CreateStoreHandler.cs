using AdventureAPI.Core.Aggregates.StoreAggregate;

namespace AdventureAPI.UseCases.Stores.Create;

public class CreateStoreHandler(IRepository<Store> repository)
    : ICommandHandler<CreateStoreCommand, Result<Store>>
{
    public async Task<Result<Store>> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        var newStore = new Store(request.Name, request.CreateUser, request.Logo);
        newStore.UpdateAddress(request.Address, request.CreateUser);
        var createdItem = await repository.AddAsync(newStore, cancellationToken);
        return new Result<Store>(createdItem);
    }
}

namespace AdventureAPI.UseCases.Stores.Get;

public record GetStoreQuery(Guid StoreId) : IQuery<Result<StoreDto>>;

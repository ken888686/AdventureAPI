namespace AdventureAPI.UseCases.Stores.List;

public record ListStoresQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<StoreDto>>>;

namespace AdventureAPI.UseCases.Cities.List;

public record ListCitiesQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<CityDto>>>;

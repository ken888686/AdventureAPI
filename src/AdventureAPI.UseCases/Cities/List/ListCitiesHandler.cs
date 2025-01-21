using AdventureAPI.Core.Aggregates.CityAggregate;
using AdventureAPI.Core.Aggregates.CityAggregate.Specifications;

namespace AdventureAPI.UseCases.Cities.List;

public class ListCitiesHandler(IRepository<City> repository)
    : IQueryHandler<ListCitiesQuery, Result<IEnumerable<CityDto>>>
{
    public async Task<Result<IEnumerable<CityDto>>> Handle(ListCitiesQuery request, CancellationToken cancellationToken)
    {
        var spec = new CityListSpec(request.Skip, request.Take);
        var cities = await repository.ListAsync(spec, cancellationToken);
        return Result<IEnumerable<CityDto>>.Success(
            cities.Select(x => new CityDto(x.Id, x.Name))
        );
    }
}

using AspNetCore.IQueryable.Extensions;

namespace Library.API;

public record FiltersBookInputModel(string? Category) : ICustomQueryable;

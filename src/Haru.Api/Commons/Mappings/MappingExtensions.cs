using AutoMapper.QueryableExtensions;
using Haru.Api.Commons.Bases;
using Microsoft.EntityFrameworkCore;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Haru.Api.Commons.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedBase<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
    {
        return PaginatedBase<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
    }

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
    {
        return queryable.ProjectTo<TDestination>(configuration).ToListAsync();
    }
}

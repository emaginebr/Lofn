using HotChocolate.Execution.Configuration;
using Lofn.API.GraphQL.Admin;
using Lofn.API.GraphQL.Public;
using Microsoft.Extensions.DependencyInjection;

namespace Lofn.API.GraphQL;

public static class GraphQLServiceExtensions
{
    public static IServiceCollection AddLofnGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType<PublicQuery>()
            .AddType<PublicStoreType>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        services
            .AddGraphQLServer("admin")
            .AddAuthorization()
            .AddQueryType<AdminQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}

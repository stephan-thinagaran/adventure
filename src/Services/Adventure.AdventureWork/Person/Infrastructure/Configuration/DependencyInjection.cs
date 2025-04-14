using Adventure.BuildingBlocks.Caching;
using Adventure.BuildingBlocks.Persistence.EFCore.AdventureWorks.DBContext;

using Microsoft.EntityFrameworkCore;

namespace Adventure.AdventureWork.Person.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        // Add your infrastructure services here
        // For example, if you have a DbContext, you can add it like this:
        builder.Services.AddDbContext<AdventureWorksDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AdventureWorks")));

        //Example for Redis Cache
        builder.AddRedisDistributedCache("redisCache");
        builder.AddRedisOutputCache("redisCache");
        builder.Services.AddOutputCache(options =>
        {
            // Default policy: cache for 60 seconds
            options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(60)));

            // Specific named policies
            options.AddPolicy("Short", builder => builder.Expire(TimeSpan.FromSeconds(10)));
            options.AddPolicy("Long", builder => builder.Expire(TimeSpan.FromMinutes(5)));
            options.AddPolicy("ByPersonId", builder => builder.SetVaryByRouteValue("id").Tag("person-cache").Expire(TimeSpan.FromMinutes(2)));
            options.AddPolicy("NoCache", builder => builder.NoCache());

            // You can add policies based on query strings, headers, etc.
            // options.AddPolicy("ByQuery", builder => builder.SetVaryByQuery("queryParam1", "queryParam2"));
        });
    }

    public static void RegisterDependencies(this IServiceCollection services)
    {
        services.AddSingleton<ICacheService, RedisCacheService>();
    }
}

var builder = DistributedApplication.CreateBuilder(args);

var redisCache = builder.AddRedis("redisCache");

var personService = builder.AddProject<Projects.Adventure_AdventureWork>("adventure-work")
                           .WithExternalHttpEndpoints()
                           .WithReference(redisCache)
                           .WaitFor(redisCache);
                           
builder.Build().Run();

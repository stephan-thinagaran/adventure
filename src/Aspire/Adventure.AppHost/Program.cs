var builder = DistributedApplication.CreateBuilder(args);

var redisCache = builder.AddRedis("redisCache");

var adventureWorkDb = builder.AddSqlServer("AdventureWorks2019")
                             .AddDatabase("db", "AdventureWorks2022");

var personService = builder.AddProject<Projects.Adventure_AdventureWork>("adventure-work")
                           .WithExternalHttpEndpoints()
                           .WithReference(redisCache)
                           .WaitFor(redisCache)
                           .WithReference(adventureWorkDb)
                           .WaitFor(adventureWorkDb);
                           
builder.Build().Run();


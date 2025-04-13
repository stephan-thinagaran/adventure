var builder = DistributedApplication.CreateBuilder(args);

var personService = builder.AddProject<Projects.Adventure_Person>("person")
                           .WithExternalHttpEndpoints();
                           
builder.Build().Run();

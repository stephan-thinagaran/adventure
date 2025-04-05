var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Adventure_Api>("apiService"); 

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.AspireWithResilientHttpClient_ApiService>("apiservice");

builder.AddProject<Projects.AspireWithResilientHttpClient_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();

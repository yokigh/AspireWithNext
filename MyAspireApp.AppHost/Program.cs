var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.MyAspireApp_WebApi>("myaspireapp-api");

builder.AddProject<Projects.MyAspireApp_Client>("myaspireapp-client")
    .WithReference(api); // <-- this connects Blazor to API!

builder.AddNpmApp("nextjs-client", "../myaspireapp.nextclient")
    .WithReference(api)
    .WithEnvironment("NEXT_PUBLIC_API_BASE_URL", api.GetEndpoint("http"))
    .WithEnvironment("PORT", "3000")
    .WithEndpoint(name: "http", port: 3000, scheme: "http");

builder.Build().Run();

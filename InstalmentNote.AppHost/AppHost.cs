var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.InstalmentNote>("instalmentnote");

builder.Build().Run();

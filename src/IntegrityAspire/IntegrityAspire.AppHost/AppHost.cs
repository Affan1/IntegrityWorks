using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Add Postgres container
var postgres = builder.AddPostgres("jobs-postgres")
    .WithImage("postgres");

// Create database
var jobsDb = postgres.AddDatabase("jobsdb");


// Add JobService_API project
builder.AddProject<Projects.JobService_API>("jobservice-api")
    .WithReference(jobsDb)
    .WaitFor(jobsDb);

builder.AddProject<Projects.UserService_API>("userservice-api");

builder.Build().Run();

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext configuration
builder.Services.AddDbContext<JobsDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("jobsdb"));

    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();

// Apply migrations automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<JobsDbContext>();
    db.Database.Migrate();
}

app.Run();

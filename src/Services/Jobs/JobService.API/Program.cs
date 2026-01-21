var builder = WebApplication.CreateBuilder(args);

// DbContext configuration
builder.Services.AddDbContext<JobsDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("JobServiceDatabase"));

    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();


var app = builder.Build();

//// Apply migrations automatically
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<JobsDbContext>();
//    db.Database.Migrate();
//}
app.MapCarter();

app.Run();

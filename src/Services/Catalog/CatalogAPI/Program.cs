using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);


var currentAssembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(currentAssembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(currentAssembly);

builder.Services.AddCarter();
builder.Services.AddMarten(options => { options.Connection(builder.Configuration.GetConnectionString("Database")!); })
    .UseLightweightSessions();

var app = builder.Build();

app.MapCarter();

app.Run();
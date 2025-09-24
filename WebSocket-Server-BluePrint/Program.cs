using WebSocket_Server_BluePrint.Hubs.ChatHub;
using Serilog;
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

Log.Information("Starting websocket server");
try
{
    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

    builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
    builder.Services.AddSignalR();
    builder.Host.UseSerilog((context, loggerConfig) =>
    {
        loggerConfig.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}]")
            .ReadFrom.Configuration(context.Configuration);
    });

    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.MapHub<ChatHub>("/chat");
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException" && ex.GetType().Name is not "HostAbortedException")
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Stopping websocket server");
    Log.CloseAndFlush();
}
using WebSocket_Server_BluePrint.Hubs.ChatHub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSignalR();

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
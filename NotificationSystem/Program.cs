using NotificationSystem.Abstract;
using NotificationSystem.Enums;
using NotificationSystem.Models;
using NotificationSystem.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<INotificationService, EmailNotificationService>();
services.AddScoped<INotificationService, SmsNotificationService>();
services.AddScoped<OrderService>();

services.AddScoped<INotificationChannelFactory, NotificationChannelFactory>();

var app = builder.Build();
var scope = app.Services.CreateScope();
var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();
await orderService.CreateOrderAsync("Test", new UserPreferences { NotificationChannelTypes = [NotificationChannelTypeEnum.Sms] });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

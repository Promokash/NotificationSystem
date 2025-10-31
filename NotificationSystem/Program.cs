using NotificationSystem.Abstract;
using NotificationSystem.Commands;
using NotificationSystem.Decorators;
using NotificationSystem.Observers;
using NotificationSystem.Services;
using NotificationSystem.Validation;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();

// Notification services (concrete implementations)
builder.Services.AddScoped<EmailNotificationService>();
builder.Services.AddScoped<SmsNotificationService>();

// Decorated INotificationService registrations
builder.Services.AddScoped<INotificationService>(sp =>
    new TimingNotificationDecorator(
        new RetryNotificationDecorator(
            sp.GetRequiredService<EmailNotificationService>(),
            sp.GetRequiredService<ILogger<RetryNotificationDecorator>>()),
        sp.GetRequiredService<ILogger<TimingNotificationDecorator>>()));

builder.Services.AddScoped<INotificationService>(sp =>
    new TimingNotificationDecorator(
        new RetryNotificationDecorator(
            sp.GetRequiredService<SmsNotificationService>(),
            sp.GetRequiredService<ILogger<RetryNotificationDecorator>>()),
        sp.GetRequiredService<ILogger<TimingNotificationDecorator>>()));

// Eventing (Observer pattern)
builder.Services.AddScoped<IOrderCreatedHandler, LoggingOrderHandler>();
builder.Services.AddScoped<IOrderCreatedHandler, MetricsOrderHandler>();
builder.Services.AddScoped<IEventPublisher, EventPublisher>();

// Factory gets ALL decorated services
services.AddScoped<INotificationChannelFactory>(sp =>
    new NotificationChannelFactory(
        sp.GetServices<INotificationService>().ToArray()));

// Command validation
builder.Services.AddScoped<ICommandValidator<CreateOrderCommand>, CreateOrderCommandValidator>();

// Command handler
builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, CreateOrderResult>, CreateOrderCommandHandler>();

// In-memory order storage (for State pattern later)
//builder.Services.AddSingleton<OrderRepository>();

var app = builder.Build();
var scope = app.Services.CreateScope();

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

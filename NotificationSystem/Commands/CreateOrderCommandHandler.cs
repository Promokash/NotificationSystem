using NotificationSystem.Abstract;
using NotificationSystem.Observers;
using NotificationSystem.Validation;

namespace NotificationSystem.Commands;

public class CreateOrderCommandHandler(
    ICommandValidator<CreateOrderCommand> validator,
    INotificationChannelFactory notificationFactory,
    IEventPublisher eventPublisher,
    ILogger<CreateOrderCommandHandler> logger)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    private static int _nextId = 1;

    public async Task<CreateOrderResult> HandleAsync(CreateOrderCommand command, CancellationToken ct = default)
    {
        var validation = validator.Validate(command);
        if (!validation.IsValid)
        {
            return new CreateOrderResult(null, false, string.Join("; ", validation.Errors));
        }

        IEnumerable<INotificationService> notificationServices = notificationFactory.GetServices(command.Preferences);

        try
        {
            foreach (INotificationService notificationService in notificationServices)
            {
                Models.Responses.SendResultResponse result = await notificationService.SendAsync(userId: 1, $"Ordered: {command.GoodName}");

                logger.LogInformation("Service {serviceName} Result:{resultCode}", notificationService.GetType().Name, result.ResultCode);
            }

            var orderEvent = new OrderCreated(
                OrderId: GenerateMockId(),
                GoodName: command.GoodName,
                CreatedAt: DateTime.UtcNow);

            await eventPublisher.PublishAsync(orderEvent);

            return new CreateOrderResult(_nextId, true, "Created successfully");
        }
        catch (Exception ex)
        {
            return new CreateOrderResult(null, false, ex.Message);
        }
    }

    private static int GenerateMockId() => Interlocked.Increment(ref _nextId);
}

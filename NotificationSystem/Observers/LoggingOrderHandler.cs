namespace NotificationSystem.Observers;

public class LoggingOrderHandler(ILogger<LoggingOrderHandler> logger) : IOrderCreatedHandler
{
    public Task HandleAsync(OrderCreated orderEvent)
    {
        logger.LogInformation("Order {OrderId} created for {Good}", orderEvent.OrderId, orderEvent.GoodName);
        return Task.CompletedTask;
    }
}
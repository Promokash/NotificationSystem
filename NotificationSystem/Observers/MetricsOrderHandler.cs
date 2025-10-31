namespace NotificationSystem.Observers;

public class MetricsOrderHandler : IOrderCreatedHandler
{
    public Task HandleAsync(OrderCreated orderEvent)
    {
        // Например, увеличить счётчик в Prometheus или Application Insights
        Console.WriteLine($"[METRICS] Order created: {orderEvent.OrderId}");
        return Task.CompletedTask;
    }
}

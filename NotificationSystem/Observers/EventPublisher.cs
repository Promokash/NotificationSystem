namespace NotificationSystem.Observers;

public class EventPublisher(IEnumerable<IOrderCreatedHandler> handlers) : IEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class
    {
        if (@event is OrderCreated orderCreated)
        {
            foreach (var handler in handlers)
            {
                await handler.HandleAsync(orderCreated);
            }
        }
        // В будущем можно добавить другие события
    }
}

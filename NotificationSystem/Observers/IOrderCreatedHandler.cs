namespace NotificationSystem.Observers;

public interface IOrderCreatedHandler
{
    Task HandleAsync(OrderCreated orderEvent);
}
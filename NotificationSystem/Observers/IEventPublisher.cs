namespace NotificationSystem.Observers;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : class;
}

using NotificationSystem.Models;

namespace NotificationSystem.Abstract;

public interface INotificationChannelFactory
{
    IEnumerable<INotificationService> GetServices(UserPreferences preferences);
}

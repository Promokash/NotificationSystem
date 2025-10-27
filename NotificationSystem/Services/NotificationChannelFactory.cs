using NotificationSystem.Abstract;
using NotificationSystem.Models;

namespace NotificationSystem.Services;

public class NotificationChannelFactory(IEnumerable<INotificationService> services) : INotificationChannelFactory
{
    public IEnumerable<INotificationService> GetServices(UserPreferences preferences)
    {
        List<INotificationService> requestedNotifiers = [];

        foreach (Enums.NotificationChannelTypeEnum notificationChannelType in preferences.NotificationChannelTypes)
        {
            requestedNotifiers.AddRange(services.Where(x => x.CanHandle(notificationChannelType)));
        }

        return requestedNotifiers;
    }
}

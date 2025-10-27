using NotificationSystem.Abstract;
using NotificationSystem.Models;

namespace NotificationSystem.Services;

public class OrderService(INotificationChannelFactory notificationFactory,
    ILogger<OrderService> logger)
{

    public async Task CreateOrderAsync(string goodName, UserPreferences preferences)
    {
        IEnumerable<INotificationService> notificationServices = notificationFactory.GetServices(preferences);

        foreach (INotificationService notificationService in notificationServices)
        {
            Models.Responses.SendResultResponse result = await notificationService.SendAsync(userId: 1, $"Ordered: {goodName}");

            logger.LogInformation("Service {serviceName} Result:{resultCode}", notificationService.GetType().Name, result.ResultCode);
        }
    }
}

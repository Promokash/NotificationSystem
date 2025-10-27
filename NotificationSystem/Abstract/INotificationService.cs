using NotificationSystem.Enums;
using NotificationSystem.Models.Responses;

namespace NotificationSystem.Abstract;

public interface INotificationService
{
    bool CanHandle(NotificationChannelTypeEnum notificationChannelType);

    Task<SendResultResponse> SendAsync(int userId, string message);
}

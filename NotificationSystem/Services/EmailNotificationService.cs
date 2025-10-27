using NotificationSystem.Abstract;
using NotificationSystem.Enums;
using NotificationSystem.Models.Responses;

namespace NotificationSystem.Services;

public class EmailNotificationService(ILogger<EmailNotificationService> logger) 
    : INotificationService
{
    public bool CanHandle(NotificationChannelTypeEnum notificationChannelType) => notificationChannelType is NotificationChannelTypeEnum.Email;

    public async Task<SendResultResponse> SendAsync(int userId, string message)
    {
        // имитирум отправку
        logger.LogInformation(message);
        await Task.Delay(3000);

        return new SendResultResponse(ResultsEnum.Ok);
    }
}

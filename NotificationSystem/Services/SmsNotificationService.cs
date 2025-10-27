using NotificationSystem.Abstract;
using NotificationSystem.Enums;
using NotificationSystem.Models.Responses;

namespace NotificationSystem.Services;

public class SmsNotificationService(ILogger<SmsNotificationService> logger) 
    : INotificationService
{
    public bool CanHandle(NotificationChannelTypeEnum notificationChannelType) => notificationChannelType is NotificationChannelTypeEnum.Sms;

    public async Task<SendResultResponse> SendAsync(int userId, string message)
    {
        // имитирум отправку
        logger.LogInformation(message);
        await Task.Delay(3000);

        return new SendResultResponse(ResultsEnum.Ok);
    }
}

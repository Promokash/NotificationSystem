using NotificationSystem.Abstract;
using NotificationSystem.Enums;
using NotificationSystem.Models.Responses;

namespace NotificationSystem.Decorators;

public class RetryNotificationDecorator : INotificationService
{
    private readonly INotificationService _innerService;
    private readonly ILogger<RetryNotificationDecorator> _logger;

    public RetryNotificationDecorator(
        INotificationService innerService,
        ILogger<RetryNotificationDecorator> logger)
    {
        _innerService = innerService;
        _logger = logger;
    }

    public bool CanHandle(NotificationChannelTypeEnum type) =>
        _innerService.CanHandle(type);

    public async Task<SendResultResponse> SendAsync(int userId, string message)
    {
        const int maxRetries = 3;
        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                return await _innerService.SendAsync(userId, message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Attempt {Attempt} failed: {Message}", attempt, ex.Message);
                if (attempt == maxRetries) throw;

                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt - 1))); // 1s, 2s, 4s
            }
        }

        throw new InvalidOperationException("All retry attempts failed.");
    }
}

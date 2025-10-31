using NotificationSystem.Abstract;
using NotificationSystem.Enums;
using NotificationSystem.Models.Responses;
using System.Diagnostics;

namespace NotificationSystem.Decorators;

public class TimingNotificationDecorator : INotificationService
{
    private readonly INotificationService _innerService;
    private readonly ILogger<TimingNotificationDecorator> _logger;

    public TimingNotificationDecorator(
        INotificationService innerService,
        ILogger<TimingNotificationDecorator> logger)
    {
        _innerService = innerService;
        _logger = logger;
    }

    public bool CanHandle(NotificationChannelTypeEnum type) => _innerService.CanHandle(type);

    public async Task<SendResultResponse> SendAsync(int userId, string message)
    {
        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();

        var result = await _innerService.SendAsync(userId, message);

        sw.Stop();

        _logger.LogInformation("Total handle time {sw}",sw.ElapsedMilliseconds);

        return result;
    }
}


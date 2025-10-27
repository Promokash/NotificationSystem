using NotificationSystem.Enums;

namespace NotificationSystem.Models;

public class UserPreferences
{
    public List<NotificationChannelTypeEnum> NotificationChannelTypes { get; set; } = [NotificationChannelTypeEnum.None];
}

using NotificationSystem.Models;

namespace NotificationSystem.Commands;

public record CreateOrderCommand(string GoodName, UserPreferences Preferences);

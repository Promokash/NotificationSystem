namespace NotificationSystem.Commands;

public record CreateOrderResult(int? OrderId, bool Success, string Message);
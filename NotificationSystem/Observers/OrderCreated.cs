namespace NotificationSystem.Observers;

public record OrderCreated(int OrderId, string GoodName, DateTime CreatedAt);
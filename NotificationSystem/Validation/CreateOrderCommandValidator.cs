using NotificationSystem.Commands;

namespace NotificationSystem.Validation;

public class CreateOrderCommandValidator : ICommandValidator<CreateOrderCommand>
{
    public ValidationResult Validate(CreateOrderCommand command)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(command.GoodName))
            errors.Add("GoodName is required.");

        if (command.GoodName.Length > 100)
            errors.Add("GoodName must be no longer than 100 characters.");

        if (command.Preferences?.NotificationChannelTypes == null || !command.Preferences.NotificationChannelTypes.Any())
            errors.Add("At least one notification channel must be selected.");

        return new ValidationResult(errors.Count == 0, errors.ToArray());
    }
}
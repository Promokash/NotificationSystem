using System.ComponentModel.DataAnnotations;

namespace NotificationSystem.Validation;

public interface ICommandValidator<in T>
{
    ValidationResult Validate(T instance);
}

public record ValidationResult(bool IsValid, string[] Errors);
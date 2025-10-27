namespace NotificationSystem.Models.Responses
{
    public record SendResultResponse(ResultsEnum ResultCode);

    public enum ResultsEnum
    {
        Ok,

        Error,

        Tomeout
    }
}

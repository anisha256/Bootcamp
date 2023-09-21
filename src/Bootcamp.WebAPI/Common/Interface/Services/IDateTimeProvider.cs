namespace Bootcamp.WebAPI.Common.Interface.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}

using Bootcamp.WebAPI.Common.Interface.Services;

namespace Bootcamp.WebAPI.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

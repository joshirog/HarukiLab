using Haru.Api.Commons.Interfaces;

namespace Haru.Api.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}
using OkoulChallenge.Application.Common.Interfaces;

namespace OkoulChallenge.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}

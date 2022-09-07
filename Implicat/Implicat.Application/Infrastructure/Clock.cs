namespace Implicat.Application.Infrastructure;

public class Clock : IClock
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}
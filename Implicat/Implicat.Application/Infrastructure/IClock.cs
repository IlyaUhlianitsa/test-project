namespace Implicat.Application.Infrastructure
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
    }
}
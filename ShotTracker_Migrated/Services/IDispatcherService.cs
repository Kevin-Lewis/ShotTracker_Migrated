namespace ShotTracker.Services
{
    public interface IDispatcherService
    {
        Task DispatchAsync(Func<Task> action);
    }
}

namespace ShotTracker.Services
{
    public class DispatcherService : IDispatcherService
    {
        private readonly Microsoft.Maui.Dispatching.IDispatcher _dispatcher;

        public DispatcherService(Microsoft.Maui.Dispatching.IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task DispatchAsync(Func<Task> action)
        {
            await MainThread.InvokeOnMainThreadAsync(action);
        }
    }
}

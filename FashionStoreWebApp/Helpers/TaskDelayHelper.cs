namespace FashionStoreWebApp.Helpers
{
    public class TaskDelayHelper
    {
        public delegate void HandleSetTimeout();

        public static void SetTimeout(HandleSetTimeout handleSetTimeout, int timeout)
        {
            Task.Run(async () =>
            {
                await Task.Delay(timeout);
                handleSetTimeout();
            });
        }
    }
}

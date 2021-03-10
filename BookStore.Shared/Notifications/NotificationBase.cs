namespace BookStore.Shared.Notifications
{
    public class NotificationBase
    {
        public string Message { get; }
        public NotificationBase(string message) 
        {
            Message = message;
        }
    }
}

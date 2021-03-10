namespace BookStore.Shared.Notifications
{
    public class NotificationBase
    {
        public string Message { get; }
        public int Status { get;  }
        public NotificationBase(string message, int status = 0) 
        {
            Message = message;
            Status = status;
        }
    }
}

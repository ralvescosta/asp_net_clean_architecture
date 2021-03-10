using BookStore.Shared.Notifications;

namespace BookStore.Application.Notifications
{
    public class NotFoundNotification : NotificationBase
    {
        public NotFoundNotification(string message = "NotFoundtNotification") : base(message) { }
    }
}

using BookStore.Shared.Notifications;

namespace BookStore.Application.Notifications
{
    public class UnauthorizedNotification : NotificationBase
    {
        public UnauthorizedNotification(string message = "UnauthorizedNotification") : base(message) { }
    }
}

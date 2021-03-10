using BookStore.Shared.Notifications;

namespace BookStore.Application.Notifications
{
    public class EmailAlreadyExistNotification : NotificationBase
    {
        public EmailAlreadyExistNotification(string message = "EmailAlreadyExistNotification") : base(message) { }
    }
}

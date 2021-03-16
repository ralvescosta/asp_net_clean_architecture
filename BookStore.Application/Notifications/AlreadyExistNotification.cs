using BookStore.Shared.Notifications;

namespace BookStore.Application.Notifications
{
    public class AlreadyExistNotification : NotificationBase
    {
        public AlreadyExistNotification(string message = "EmailAlreadyExistNotification") : base(message) { }
    }
}

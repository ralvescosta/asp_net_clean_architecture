using BookStore.Shared.Notifications;

namespace BookStore.Application.Notifications
{
    public class WrongPasswordNotification : NotificationBase
    {
        public WrongPasswordNotification(string message = "WrongPasswordNotification") : base(message) { }
    }
}
